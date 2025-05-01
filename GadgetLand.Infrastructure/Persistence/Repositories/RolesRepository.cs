using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class RolesRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Role>(dbContext), IRolesRepository
{
    public async Task<Role?> GetByNameAsync(string name)
    {
        return await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == name);
    }
}
