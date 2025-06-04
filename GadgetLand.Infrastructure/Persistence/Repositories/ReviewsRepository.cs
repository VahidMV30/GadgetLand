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
}
