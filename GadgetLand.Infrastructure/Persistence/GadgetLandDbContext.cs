using System.Reflection;
using GadgetLand.Application.Interfaces;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence;

public class GadgetLandDbContext(DbContextOptions<GadgetLandDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}
