using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using GadgetLand.Domain.Entities;
using MediatR;

namespace GadgetLand.Application.Features.Products.Commands.ModifyProductImages;

public class ModifyProductImagesCommandHandler(
    IProductsRepository productsRepository,
    IImageUploader imageUploader,
    IUnitOfWork unitOfWork) : IRequestHandler<ModifyProductImagesCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(ModifyProductImagesCommand request, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(request.Id);

        if (product is null) return ProductErrors.NotFound;

        if (request.ImagesToRemove is { Length: > 0 } images && images.Any(x => string.IsNullOrWhiteSpace(x) is false))
        {
            var imagesToRemove = await productsRepository.GetProductImagesByFileNamesAsync(request.ImagesToRemove);
            productsRepository.RemoveProductImages(imagesToRemove);

            foreach (var imageToRemove in request.ImagesToRemove)
            {
                imageUploader.DeleteImage(imageToRemove, "productImages");
            }
        }

        if (request.NewImages is not null && request.NewImages.Length > 0)
        {
            var uploadedFileNames = await imageUploader.UploadImagesAsync(request.NewImages, "productImages");

            var productImages = uploadedFileNames.Select(uploadedFileName => new ProductImage { ProductId = request.Id, Image = uploadedFileName }).ToList();

            await productsRepository.CreateProductImagesAsync(productImages);
        }

        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("تغییرات برای گالری تصاویر ذخیره شد.");
    }
}
