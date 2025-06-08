using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Reviews;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Queries.GetReviewDetailsById;

public class GetReviewDetailsByIdQueryHandler(IReviewsRepository reviewsRepository, IMapper mapper) : IRequestHandler<GetReviewDetailsByIdQuery, ErrorOr<ReviewDetailsResponse>>
{
    public async Task<ErrorOr<ReviewDetailsResponse>> Handle(GetReviewDetailsByIdQuery query, CancellationToken cancellationToken)
    {
        var review = await reviewsRepository.GetReviewByIdAsync(query.Id);

        if (review is null) return ReviewErrors.NotFound;

        return mapper.Map<ReviewDetailsResponse>(review);
    }
}
