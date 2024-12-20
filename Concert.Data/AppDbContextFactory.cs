using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ConcertBookingApp.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContextFactory() { }

        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var connectionString = builder.GetConnectionString("ConcertDBContext");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("The connection string was not set.");

            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString).Options;
            return new AppDbContext(options);
        }
    }
}

