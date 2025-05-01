using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IUsersRepository : IBaseRepository<int, User>
{
    Task<User?> GetByEmailAsync(string email);
}
