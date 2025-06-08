using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Commands.ToggleConformationStatus;

public class ToggleConformationStatusCommandHandler(
    IReviewsRepository reviewsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ToggleConformationStatusCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(ToggleConformationStatusCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewsRepository.GetByIdAsync(request.Id);

        if (review is null) return ReviewErrors.NotFound;

        review.IsConfirmed = !review.IsConfirmed;

        reviewsRepository.Update(review);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("وضعیت تائید دیدگاه با موفقیت تغییر کرد.");
    }
}
