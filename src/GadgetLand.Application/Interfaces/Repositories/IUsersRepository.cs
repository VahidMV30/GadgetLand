using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task CreateAsync(User user);
}
