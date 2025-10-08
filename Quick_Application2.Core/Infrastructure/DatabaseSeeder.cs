

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
            await SeedInmatesAsync(dbContext); 
      
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
            if (await db.Inmates.AnyAsync()) return;

            var sacJail = await db.Jails.FirstAsync(j => j.Name == "Sacramento County Main Jail");
            var sfJail = await db.Jails.FirstAsync(j => j.Name == "San Francisco County Jail Complex");

            var inmates = new[]
            {
        new Inmate
        {
            ExternalId = "SCMJ-001",
            FirstName = "Alex",
            LastName = "Rivera",
            DateOfBirth = new DateTime(1985, 6, 15),
            JailId = sacJail.Id,
            Jail = sacJail,
            Status = "0",                     // e.g., Active / Booked
            BookingDate = DateTime.UtcNow.AddDays(-14),
            CellId = null
        },
        new Inmate
        {
            ExternalId = "SFCJ-002",
            FirstName = "Maria",
            LastName = "Nguyen",
            DateOfBirth = new DateTime(1991, 11, 5),
            JailId = sfJail.Id,
            Jail = sfJail,
            Status = "0",                     // e.g., Active / Booked
            BookingDate = DateTime.UtcNow.AddDays(-7),
            CellId = null
        }
    };

            db.Inmates.AddRange(inmates);
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
