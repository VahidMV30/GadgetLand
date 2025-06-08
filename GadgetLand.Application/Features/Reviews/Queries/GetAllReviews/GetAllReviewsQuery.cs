using GadgetLand.Contracts.Reviews;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Queries.GetAllReviews;

public record GetAllReviewsQuery() : IRequest<IEnumerable<ReviewResponse>>;
