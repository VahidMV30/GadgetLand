using GadgetLand.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GadgetLand.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GadgetLandDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("GadgetLand")));

        return services;
    }
}
