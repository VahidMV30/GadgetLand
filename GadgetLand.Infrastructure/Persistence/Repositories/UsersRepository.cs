using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class UsersRepository(GadgetLandDbContext dbContext) : BaseRepository<int, User>(dbContext), IUsersRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetUserAddressInfoByIdAsync(int id)
    {
        return await dbContext.Users
            .Include(x => x.City)
            .ThenInclude(x => x!.Province)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await dbContext.Users
            .Include(user => user.City)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            .ThenInclude(city => city.Province)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetUserDetailsWithOrdersAsync(int userId)
    {
        return await dbContext.Users
            .Include(user => user.City)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            .ThenInclude(city => city.Province)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            .Include(user => user.Orders)
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == userId);
    }
}
