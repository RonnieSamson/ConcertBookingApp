using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Concert.Data;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContextFactory() { }
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("json1.json").Build();
        var connectionString = builder.GetConnectionString("ConcertBookingApp");
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("The connection string was not set.");
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer(connectionString).Options;
        return new ApplicationDbContext(options);
    }
}
