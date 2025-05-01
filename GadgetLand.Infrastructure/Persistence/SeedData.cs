using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GadgetLand.Infrastructure.Persistence;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var dbContext = new GadgetLandDbContext(serviceProvider.GetRequiredService<DbContextOptions<GadgetLandDbContext>>());

        if (dbContext.Roles.Any() is false)
        {
            var roles = new List<Role>
            {
                new() { Name = configuration["AuthSettings:Roles:Admin"]! },
                new() { Name=configuration["AuthSettings:Roles:User"]! }
            };

            dbContext.Roles.AddRange(roles);
            dbContext.SaveChanges();
        }

        if (dbContext.Users.Any()) return;

        dbContext.Users.Add(new User
        {
            RoleId = 1,
            FullName = configuration["AuthSettings:AdminUser:FullName"]!,
            Email = configuration["AuthSettings:AdminUser:Email"]!,
            Password = BCrypt.Net.BCrypt.HashPassword(configuration["AuthSettings:AdminUser:Password"]!),
        });

        dbContext.SaveChanges();
    }
}
