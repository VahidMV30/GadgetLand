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
}
