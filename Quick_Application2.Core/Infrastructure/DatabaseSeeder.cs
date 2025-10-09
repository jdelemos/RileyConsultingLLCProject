

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quick_Application2.Core.Models;
using Quick_Application2.Core.Models.Account;
using Quick_Application2.Core.Models.Jms;
using Quick_Application2.Core.Models.JMS;
using Quick_Application2.Core.Models.Shop;
using Quick_Application2.Core.Services.Account;

namespace Quick_Application2.Core.Infrastructure
{
    public class DatabaseSeeder(ApplicationDbContext dbContext, ILogger<DatabaseSeeder> logger,
        IUserAccountService userAccountService, IUserRoleService userRoleService) : IDatabaseSeeder
    {
        public async Task SeedAsync()
        {
            await dbContext.Database.MigrateAsync();
            await SeedDefaultUsersAsync();
            await SeedDemoDataAsync();
            await SeedJailsAsync(dbContext);
            await SeedCells(dbContext);
            await SeedInmatesAsync(dbContext);
            await SeedTransfersAsync(dbContext);
            await SeedBookingsAsync(dbContext);


        }

        /************ DEFAULT USERS **************/

        private async Task EnsureUserAsync(
    string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            var existingUser = await userAccountService.GetUserByUsernameAsync(userName);
            if (existingUser != null)
            {
                return; // already exists, skip
            }

            var applicationUser = new ApplicationUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await userAccountService.CreateUserAsync(applicationUser, roles, password);

            if (!result.Succeeded)
            {
                throw new UserAccountException($"Seeding \"{userName}\" user failed. Errors: " +
                    $"{string.Join(Environment.NewLine, result.Errors)}");
            }
        }

        private async Task SeedDefaultUsersAsync()
        {
            const string adminRoleName = "administrator";
            const string userRoleName = "user";

            await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
            await EnsureRoleAsync(userRoleName, "Default user", []);

            await EnsureUserAsync("admin", "TempP@ss123", "Inbuilt Administrator", "admin@company.com", "+1 (123) 000-0000", [adminRoleName]);
            await EnsureUserAsync("jdelemos", "TempP@ss123", "Inbuilt Administrator", "jdelemos@company.com", "+1 (123) 000-0000", [adminRoleName]);
            await EnsureUserAsync("user", "TempP@ss123", "Inbuilt Standard User", "user@company.com", "+1 (123) 000-0001", [userRoleName]);
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if (await userRoleService.GetRoleByNameAsync(roleName) == null)
            {
                logger.LogInformation("Generating default role: {roleName}", roleName);

                var applicationRole = new ApplicationRole(roleName, description);

                var result = await userRoleService.CreateRoleAsync(applicationRole, claims);

                if (!result.Succeeded)
                {
                    throw new UserRoleException($"Seeding \"{description}\" role failed. Errors: " +
                        $"{string.Join(Environment.NewLine, result.Errors)}");
                }
            }
        }

        private async Task<ApplicationUser> CreateUserAsync(
            string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            logger.LogInformation("Generating default user: {userName}", userName);

            var applicationUser = new ApplicationUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await userAccountService.CreateUserAsync(applicationUser, roles, password);

            if (!result.Succeeded)
            {
                throw new UserAccountException($"Seeding \"{userName}\" user failed. Errors: " +
                    $"{string.Join(Environment.NewLine, result.Errors)}");
            }

            return applicationUser;
        }

        
        /************ Jon's DATA **************/
        public async Task SeedJailsAsync(ApplicationDbContext db)
        {
            if (await db.Jails.AnyAsync()) return;

            var jails = new[]
            {
        new Jail
        {
            Name = "Sacramento County Main Jail",
            City = "Sacramento",
            State = "CA",
            Zip = "95814",
            OpenedYear = 1989,
            Capacity = 4100,
            Type = JailType.CountyJail,
            Security = SecurityLevel.Maximum,
            Status = JailStatus.Operational,
            Description = "Primary intake and housing facility for Sacramento County.",
            FeaturesCsv = "Medical Unit,Mental Health,Classification,Video Visitation"
        },
        new Jail
        {
            Name = "San Francisco County Jail Complex",
            City = "San Francisco",
            State = "CA",
            Zip = "94124",
            OpenedYear = 1994,
            Capacity = 2300,
            Type = JailType.CountyJail,
            Security = SecurityLevel.MultiLevel,
            Status = JailStatus.AtCapacity,
            Description = "Bayview district jail complex serving pre-trial and sentenced inmates.",
            FeaturesCsv = "Education Programs,Substance Abuse,Reentry Services,Work Programs"
        }
    };

            db.Jails.AddRange(jails);
            await db.SaveChangesAsync();
        }

        public async Task SeedInmatesAsync(ApplicationDbContext db)
        {
            // 1️⃣  Get reference data
            var sacJail = await db.Jails.FirstAsync(j => j.Name == "Sacramento County Main Jail");
            var cells = await db.Cells.Where(c => c.JailId == sacJail.Id).ToListAsync();

            // 2️⃣  Define your desired inmate list (including new ones)
            var desiredInmates = new List<Inmate>
    {
        new Inmate
        {
            ExternalId  = "SCMJ-001",
            FirstName   = "Alex",
            LastName    = "Rivera",
            Status      = "0",
            JailId      = sacJail.Id,
            CellId      = cells.First(c => c.CellNumber == "A101").Id,
            BookingDate = DateTime.UtcNow.AddDays(-5)
        },
        new Inmate
        {
            ExternalId  = "SCMJ-002",
            FirstName   = "Maria",
            LastName    = "Nguyen",
            Status      = "0",
            JailId      = sacJail.Id,
            CellId      = cells.First(c => c.CellNumber == "B201").Id,
            BookingDate = DateTime.UtcNow.AddDays(-3)
        },
        new Inmate
        {
            ExternalId  = "SCMJ-003",
            FirstName   = "Jamal",
            LastName    = "Reed",
            Status      = "0",
            JailId      = sacJail.Id,
            CellId      = cells.First(c => c.CellNumber == "C301").Id,
            BookingDate = DateTime.UtcNow.AddDays(-1)
        },
        // 🆕  New inmates for seeding
        new Inmate
        {
            ExternalId  = "SCMJ-004",
            FirstName   = "Sofia",
            LastName    = "Lopez",
            Status      = "0",
            JailId      = sacJail.Id,
            CellId      = cells.First(c => c.CellNumber == "A102").Id,
            BookingDate = DateTime.UtcNow.AddDays(-2)
        },
        new Inmate
        {
            ExternalId  = "SCMJ-005",
            FirstName   = "David",
            LastName    = "Kim",
            Status      = "0",
            JailId      = sacJail.Id,
            CellId      = cells.First(c => c.CellNumber == "B202").Id,
            BookingDate = DateTime.UtcNow.AddDays(-4)
        }
    };

            // 3️⃣  Fetch existing inmate IDs
            var existingIds = await db.Inmates.Select(i => i.ExternalId).ToListAsync();

            // 4️⃣  Add only missing inmates
            var newInmates = desiredInmates
                .Where(i => !existingIds.Contains(i.ExternalId))
                .ToList();

            if (newInmates.Count > 0)
            {
                await db.Inmates.AddRangeAsync(newInmates);
                await db.SaveChangesAsync();
                Console.WriteLine($"Added {newInmates.Count} new inmate(s).");
            }

            // 5️⃣  Assign missing CellIds to existing inmates
            var missingCellAssignments = await db.Inmates
                .Where(i => i.CellId == null)
                .ToListAsync();

            foreach (var inmate in missingCellAssignments)
            {
                // Default logic: pick a free cell (not occupied)
                var openCell = cells.FirstOrDefault(c => !c.IsOccupied);
                if (openCell != null)
                {
                    inmate.CellId = openCell.Id;
                    openCell.IsOccupied = true;
                }
            }

            // 6️⃣  Mark all occupied cells appropriately
            var allInmates = await db.Inmates.ToListAsync();
            foreach (var cell in cells)
                cell.IsOccupied = allInmates.Any(i => i.CellId == cell.Id);

            await db.SaveChangesAsync();
        }





        public async Task SeedTransfersAsync(ApplicationDbContext db)
        {
            if (await db.Transfers.AnyAsync()) return;

            var sacJail = await db.Jails.FirstAsync(j => j.Name == "Sacramento County Main Jail");
            var sfJail = await db.Jails.FirstAsync(j => j.Name == "San Francisco County Jail Complex");
            var inmate = await db.Inmates.FirstAsync(i => i.ExternalId == "SFCJ-002");

            var transfer = new Transfer
            {
                InmateId = inmate.Id,
                FromJailId = sfJail.Id,
                ToJailId = sacJail.Id,
                Reason = "Court appearance transfer",
                TransferDate = DateTime.UtcNow
            };

            db.Transfers.Add(transfer);
            await db.SaveChangesAsync();
        }

        public async Task SeedBookingsAsync(ApplicationDbContext db)
        {
            if (await db.Bookings.AnyAsync()) return;

            // Pull in existing data
            var sacJail = await db.Jails.FirstAsync(j => j.Name == "Sacramento County Main Jail");
            var sfJail = await db.Jails.FirstAsync(j => j.Name == "San Francisco County Jail Complex");

            var alex = await db.Inmates.FirstAsync(i => i.FirstName == "Alex" && i.LastName == "Rivera");
            var maria = await db.Inmates.FirstAsync(i => i.FirstName == "Maria" && i.LastName == "Nguyen");

            var bookings = new[]
            {
        new Booking
        {
            JailId   = sacJail.Id,
            InmateId = alex.Id,
            IntakeDate = DateTime.UtcNow.AddDays(-3),
            Status = BookingStatus.Active,
            Charges = new List<Charge>
            {
                new Charge { Offense = "Embezzlement", BondAmount = 15000, Statute = "PC 240" },
                new Charge { Offense = "Fraud", BondAmount = 500, Statute = "PC 647" }
            },
            Bond = new Bond { Amount = 15500, IsPaid = false, Type = "Cash" }
        },
        new Booking
        {
            JailId   = sacJail.Id,
            InmateId = maria.Id,
            IntakeDate = DateTime.UtcNow.AddDays(-7),
            Status = BookingStatus.OnHold,
            Charges = new List<Charge>
            {
                new Charge { Offense = "Unauthorized Access to Computer and Fraud", BondAmount = 20000, Statute = "PC 459" }
            },
            Holds = new List<Hold>
            {
                new Hold { Agency = "ICE", Reason = "Federal detainer", CreatedAt = DateTime.UtcNow }
            }
        }
    };

            db.Bookings.AddRange(bookings);
            await db.SaveChangesAsync();
        }



        public async Task SeedCells(ApplicationDbContext db)
        {
            if (await db.Cells.AnyAsync()) return;

            // Find the jail to attach the cells/units to
            var sacJail = await db.Jails.FirstAsync(j => j.Name == "Sacramento County Main Jail");

            // Create a few units (A, B, C) for this jail
            var units = new[]
            {
        new Unit { Name = "Unit A", JailId = sacJail.Id },
        new Unit { Name = "Unit B", JailId = sacJail.Id },
        new Unit { Name = "Unit C", JailId = sacJail.Id }
    };

            // Add units to the context
            await db.Units.AddRangeAsync(units);
            await db.SaveChangesAsync(); // Save so we have real Unit.Id values

            // Create cells and assign them to units
            var cells = new[]
            {
        new Cell { CellNumber = "A101", JailId = sacJail.Id, UnitId = units[0].Id },
        new Cell { CellNumber = "A102", JailId = sacJail.Id, UnitId = units[0].Id },
        new Cell { CellNumber = "B201", JailId = sacJail.Id, UnitId = units[1].Id },
        new Cell { CellNumber = "B202", JailId = sacJail.Id, UnitId = units[1].Id },
        new Cell { CellNumber = "C301", JailId = sacJail.Id, UnitId = units[2].Id },
        new Cell { CellNumber = "C302", JailId = sacJail.Id, UnitId = units[2].Id },
    };

            await db.Cells.AddRangeAsync(cells);
            await db.SaveChangesAsync();
        }




        private async Task SeedDemoDataAsync()
        {
            
            if (!await dbContext.Customers.AnyAsync() && !await dbContext.ProductCategories.AnyAsync())
            {
                logger.LogInformation("Seeding demo data");

                var cust_1 = new Customer
                {
                    Name = "Ebenezer Monney",
                    Email = "contact@ebenmonney.com",
                    Gender = Gender.Male
                };

                var cust_2 = new Customer
                {
                    Name = "Itachi Uchiha",
                    Email = "uchiha@narutoverse.com",
                    PhoneNumber = "+81123456789",
                    Address = "Some fictional Address, Street 123, Konoha",
                    City = "Konoha",
                    Gender = Gender.Male
                };

                var cust_3 = new Customer
                {
                    Name = "John Doe",
                    Email = "johndoe@anonymous.com",
                    PhoneNumber = "+18585858",
                    Address = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                    Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at elementum imperdiet",
                    City = "Lorem Ipsum",
                    Gender = Gender.Male
                };

                var cust_4 = new Customer
                {
                    Name = "Jane Doe",
                    Email = "Janedoe@anonymous.com",
                    PhoneNumber = "+18585858",
                    Address = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                    Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at elementum imperdiet",
                    City = "Lorem Ipsum",
                    Gender = Gender.Male
                };

                var prodCat_1 = new ProductCategory
                {
                    Name = "None",
                    Description = "Default category. Products that have not been assigned a category"
                };

                var prod_1 = new Product
                {
                    Name = "BMW M6",
                    Description = "Yet another masterpiece from the world's best car manufacturer",
                    BuyingPrice = 109775,
                    SellingPrice = 114234,
                    UnitsInStock = 12,
                    IsActive = true,
                    ProductCategory = prodCat_1
                };

                var prod_2 = new Product
                {
                    Name = "Nissan Patrol",
                    Description = "A true man's choice",
                    BuyingPrice = 78990,
                    SellingPrice = 86990,
                    UnitsInStock = 4,
                    IsActive = true,
                    ProductCategory = prodCat_1
                };

                var ordr_1 = new Order
                {
                    Discount = 500,
                    Cashier = await dbContext.Users.OrderBy(u => u.UserName).FirstAsync(),
                    Customer = cust_1
                };

                var ordr_2 = new Order
                {
                    Cashier = await dbContext.Users.OrderBy(u => u.UserName).FirstAsync(),
                    Customer = cust_2
                };

                ordr_1.OrderDetails.Add(new()
                {
                    UnitPrice = prod_1.SellingPrice,
                    Quantity = 1,
                    Product = prod_1,
                    Order = ordr_1
                });
                ordr_1.OrderDetails.Add(new()
                {
                    UnitPrice = prod_2.SellingPrice,
                    Quantity = 1,
                    Product = prod_2,
                    Order = ordr_1
                });

                ordr_2.OrderDetails.Add(new()
                {
                    UnitPrice = prod_2.SellingPrice,
                    Quantity = 1,
                    Product = prod_2,
                    Order = ordr_2
                });

                dbContext.Customers.Add(cust_1);
                dbContext.Customers.Add(cust_2);
                dbContext.Customers.Add(cust_3);
                dbContext.Customers.Add(cust_4);

                dbContext.Products.Add(prod_1);
                dbContext.Products.Add(prod_2);

                dbContext.Orders.Add(ordr_1);
                dbContext.Orders.Add(ordr_2);

                await dbContext.SaveChangesAsync();

                logger.LogInformation("Seeding demo data completed");
            }
        }
    }
}
