using ErrorOr;
using GadgetLand.Contracts.Reviews;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Queries.GetReviewDetailsById;

public record GetReviewDetailsByIdQuery(int Id) : IRequest<ErrorOr<ReviewDetailsResponse>>;
