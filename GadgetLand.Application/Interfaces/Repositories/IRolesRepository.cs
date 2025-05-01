using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IRolesRepository : IBaseRepository<int, Role>
{
    Task<Role?> GetByNameAsync(string name);
}
