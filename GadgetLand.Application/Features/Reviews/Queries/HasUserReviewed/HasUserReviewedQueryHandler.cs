using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Queries.HasUserReviewed;

public class HasUserReviewedQueryHandler(ISecurityService securityService, IReviewsRepository reviewsRepository) : IRequestHandler<HasUserReviewedQuery, bool>
{
    public async Task<bool> Handle(HasUserReviewedQuery query, CancellationToken cancellationToken)
    {
        var userId = Convert.ToInt32(securityService.GetUserIdFromToken());

        var review = await reviewsRepository.HasUserReviewedAsync(userId, query.ProductId);

        return review is not null;
    }
}
