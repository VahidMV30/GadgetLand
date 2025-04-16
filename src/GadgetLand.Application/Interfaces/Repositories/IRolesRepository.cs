using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IRolesRepository : IRepositoryBase<int, Role>
{
    Task<Role?> GetByNameAsync(string name);
}
