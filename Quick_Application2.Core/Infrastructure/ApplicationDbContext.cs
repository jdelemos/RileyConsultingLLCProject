

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quick_Application2.Core.Models;
using Quick_Application2.Core.Models.Account;
using Quick_Application2.Core.Models.Jms;
using Quick_Application2.Core.Models.Shop;
using Quick_Application2.Core.Services.Account;
using Quick_Application2.Core.Models.JMS;

namespace Quick_Application2.Core.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions options, IUserIdAccessor userIdAccessor) :
        IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Jail> Jails => Set<Jail>();
        public DbSet<Inmate> Inmates => Set<Inmate>();
        public DbSet<Unit> Units { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Bond> Bonds { get; set; }
        public DbSet<Hold> Holds { get; set; }





        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";
            const string tablePrefix = "";

            // Existing mappings
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>()
                .HasMany(r => r.Claims)
                .WithOne()
                .HasForeignKey(c => c.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>()
                .HasMany(r => r.Users)
                .WithOne()
                .HasForeignKey(r => r.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.Name);
            builder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
            builder.Entity<Customer>().Property(c => c.PhoneNumber).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Customer>().Property(c => c.City).HasMaxLength(50);
            builder.Entity<Customer>().ToTable($"{nameof(Customers)}");

            builder.Entity<ProductCategory>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<ProductCategory>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<ProductCategory>().ToTable($"{nameof(ProductCategories)}");

            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Product>().HasIndex(p => p.Name);
            builder.Entity<Product>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<Product>().Property(p => p.Icon).IsUnicode(false).HasMaxLength(256);
            builder.Entity<Product>().HasOne(p => p.Parent).WithMany(p => p.Children).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>().Property(p => p.BuyingPrice).HasColumnType(priceDecimalType);
            builder.Entity<Product>().Property(p => p.SellingPrice).HasColumnType(priceDecimalType);
            builder.Entity<Product>().ToTable($"{nameof(Products)}");

            builder.Entity<Order>().Property(o => o.Comments).HasMaxLength(500);
            builder.Entity<Order>().Property(p => p.Discount).HasColumnType(priceDecimalType);
            builder.Entity<Order>().ToTable($"{nameof(Orders)}");

            builder.Entity<OrderDetail>().Property(p => p.UnitPrice).HasColumnType(priceDecimalType);
            builder.Entity<OrderDetail>().Property(p => p.Discount).HasColumnType(priceDecimalType);
            builder.Entity<OrderDetail>().ToTable($"{nameof(OrderDetails)}");

            builder.Entity<Jail>(entity =>
            {
                entity.Property(j => j.Name).IsRequired().HasMaxLength(200);
                entity.HasIndex(j => new { j.Name, j.City }).IsUnique();

                entity.Property(j => j.City).HasMaxLength(80);
                entity.Property(j => j.State).HasMaxLength(2);
                entity.Property(j => j.Zip).HasMaxLength(10);
                entity.Property(j => j.Description).HasMaxLength(2000);
                entity.Property(j => j.FeaturesCsv).HasMaxLength(1000);

                entity.Property(j => j.Type).HasConversion<int>();
                entity.Property(j => j.Security).HasConversion<int>();
                entity.Property(j => j.Status).HasConversion<int>();

                entity.ToTable($"{nameof(Jails)}");
            });

            builder.Entity<Inmate>(entity =>
            {
                entity.Property(i => i.ExternalId).IsRequired().HasMaxLength(32);
                entity.HasIndex(i => i.ExternalId).IsUnique();
                entity.HasIndex(i => new { i.LastName, i.FirstName });
                entity.HasIndex(i => i.BookingDate);

                entity.Property(i => i.Status).HasConversion<int>();
                entity.Property(i => i.FirstName).IsRequired().HasMaxLength(80);
                entity.Property(i => i.LastName).IsRequired().HasMaxLength(80);

                entity.HasOne(i => i.Jail)
                      .WithMany(j => j.Inmates)
                      .HasForeignKey(i => i.JailId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable($"{nameof(Inmates)}");
            });

            // ===========================================================
            // UNIT CONFIGURATION
            // ===========================================================
            builder.Entity<Unit>(entity =>
            {
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => new { u.Name, u.JailId }).IsUnique();

                entity.HasOne(u => u.Jail)
                      .WithMany(j => j.Units)
                      .HasForeignKey(u => u.JailId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable($"{nameof(Units)}");
            });

            // ===========================================================
            // CELL CONFIGURATION
            // ===========================================================
            builder.Entity<Cell>(entity =>
            {
                entity.Property(c => c.CellNumber).IsRequired().HasMaxLength(50);
                entity.HasIndex(c => new { c.CellNumber, c.JailId }).IsUnique();

                entity.HasOne(c => c.Jail)
                      .WithMany(j => j.Cells)
                      .HasForeignKey(c => c.JailId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Unit)
                      .WithMany(u => u.Cells)
                      .HasForeignKey(c => c.UnitId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable($"{nameof(Cells)}");
            });

            // ===========================================================
            // TRANSFER CONFIGURATION
            // ===========================================================
            builder.Entity<Transfer>(entity =>
            {
                entity.Property(t => t.Reason).HasMaxLength(500);

                entity.HasOne(t => t.Inmate)
                      .WithMany(i => i.Transfers)
                      .HasForeignKey(t => t.InmateId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.FromJail)
                      .WithMany(j => j.Transfers)
                      .HasForeignKey(t => t.FromJailId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.ToJail)
                      .WithMany()
                      .HasForeignKey(t => t.ToJailId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable($"{nameof(Transfers)}");
            });

            builder.Entity<Booking>(entity =>
            {
                entity.Property(b => b.ReleaseReason).HasMaxLength(200);

                entity.HasOne(b => b.Inmate)
                    .WithMany(i => i.Bookings)
                    .HasForeignKey(b => b.InmateId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Jail)
                    .WithMany(j => j.Bookings)
                    .HasForeignKey(b => b.JailId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Charge>()
                .HasOne(c => c.Booking)
                .WithMany(b => b.Charges)
                .HasForeignKey(c => c.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Bond>()
                .HasOne(bd => bd.Booking)
                .WithOne(b => b.Bond)
                .HasForeignKey<Bond>(bd => bd.BookingId);

            builder.Entity<Hold>()
                .HasOne(h => h.Booking)
                .WithMany(b => b.Holds)
                .HasForeignKey(h => h.BookingId);

        }


        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddAuditInfo()
        {
            var currentUserId = userIdAccessor.GetCurrentUserId();

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity &&
                           (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = currentUserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = currentUserId;
            }
        }
    }
}
