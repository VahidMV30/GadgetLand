using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IRolesRepository
{
    Task<Role?> GetByNameAsync(string name);
}
