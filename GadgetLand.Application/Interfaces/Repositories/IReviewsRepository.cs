using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IReviewsRepository : IBaseRepository<int, Review>
{
    Task<Review?> HasUserReviewedAsync(int userId, int productId);
}
