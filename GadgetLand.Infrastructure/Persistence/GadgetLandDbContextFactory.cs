using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GadgetLand.Infrastructure.Persistence;

public class GadgetLandDbContextFactory : IDesignTimeDbContextFactory<GadgetLandDbContext>
{
    public GadgetLandDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<GadgetLandDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("GadgetLand"));
        return new GadgetLandDbContext(optionsBuilder.Options);
    }
}
