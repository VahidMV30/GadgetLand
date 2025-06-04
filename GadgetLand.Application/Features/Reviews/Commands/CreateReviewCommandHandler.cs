using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using GadgetLand.Domain.Entities;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Commands;

public class CreateReviewCommandHandler(
    IProductsRepository productsRepository,
    IMapper mapper,
    ISecurityService securityService,
    IReviewsRepository reviewsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateReviewCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = Convert.ToInt32(securityService.GetUserIdFromToken());
        var productId = request.ProductId;

        var product = await productsRepository.GetProductByIdAsync(productId);

        if (product is null) return ProductErrors.NotFound;

        if (await reviewsRepository.HasUserReviewedAsync(userId, productId) is not null) return ReviewErrors.Duplicate;

        var review = mapper.Map<Review>(request);
        review.UserId = userId;

        await reviewsRepository.CreateAsync(review);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("دیدگاه با موفقیت ارسال شد.");
    }
}
