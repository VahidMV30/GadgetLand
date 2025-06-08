using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler(IReviewsRepository reviewsRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteReviewCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewsRepository.GetByIdAsync(request.Id);

        if (review is null) return ReviewErrors.NotFound;

        review.IsDeleted = true;

        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("دیدگاه با موفقیت حذف شد.");
    }
}
