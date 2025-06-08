using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Reviews;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Queries.GetAllReviews;

public class GetAllReviewsQueryHandler(IReviewsRepository reviewsRepository, IMapper mapper) : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewResponse>>
{
    public async Task<IEnumerable<ReviewResponse>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken)
    {
        var reviews = await reviewsRepository.GetAllReviewsAsync();

        return mapper.Map<IEnumerable<ReviewResponse>>(reviews);
    }
}
