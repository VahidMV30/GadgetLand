using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class ReviewsRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Review>(dbContext), IReviewsRepository
{
    public async Task<Review?> HasUserReviewedAsync(int userId, int productId)
    {
        return await dbContext.Reviews.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task<IEnumerable<Review>> GetAllReviewsAsync()
    {
        return await dbContext.Reviews
            .Include(x => x.User)
            .Include(x => x.Product)
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.IsConfirmed == false)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Review?> GetReviewByIdAsync(int id)
    {
        return await dbContext.Reviews
            .Include(x => x.User)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
