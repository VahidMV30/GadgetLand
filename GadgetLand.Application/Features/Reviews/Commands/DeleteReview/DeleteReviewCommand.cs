using ErrorOr;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand(int Id) : IRequest<ErrorOr<OperationResponse>>;
