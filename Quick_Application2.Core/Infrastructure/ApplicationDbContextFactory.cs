using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace Quick_Application2.Core.Infrastructure
{
    // Used by EF CLI tools to create your context when running commands
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Adjust path so EF can find appsettings.json inside the Server project
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Quick_Application2.Server");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly("Quick_Application2.Core") // 👈 explicitly set here
            );

            return new ApplicationDbContext(optionsBuilder.Options, null!);
        }
    }
}
