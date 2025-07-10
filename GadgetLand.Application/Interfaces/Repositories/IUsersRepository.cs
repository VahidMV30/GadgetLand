using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IUsersRepository : IBaseRepository<int, User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetUserAddressInfoByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserDetailsWithOrdersAsync(int userId);
    Task<User?> GetUserDetailsByIdAsync(int userId);
}
