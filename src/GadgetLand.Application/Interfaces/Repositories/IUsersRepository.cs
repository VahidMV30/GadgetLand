using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IUsersRepository : IRepositoryBase<int, User>
{
    Task<User?> GetByEmailAsync(string email);
}
