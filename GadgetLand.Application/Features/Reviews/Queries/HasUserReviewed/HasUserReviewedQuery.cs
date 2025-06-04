using MediatR;

namespace GadgetLand.Application.Features.Reviews.Queries.HasUserReviewed;

public record HasUserReviewedQuery(int ProductId) : IRequest<bool>;
