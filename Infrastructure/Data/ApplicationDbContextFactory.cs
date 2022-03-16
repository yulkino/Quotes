using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath("D:/Garbage_Collector/Programming/Quotes/Quotes")
            .AddJsonFile("appsettings.json")
            .Build();

        var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnectionString");

        dbContextBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(dbContextBuilder.Options);
    }
}