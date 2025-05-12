using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductsRepository productsRepository,
    IMapper mapper,
    IImageUploader imageUploader,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        request = request with { Name = request.Name.NormalizeSpaces(), Slug = request.Slug.Slugify() };

        var product = await productsRepository.GetByIdAsync(request.Id);

        if (product is null) return ProductErrors.NotFound;

        if (await productsRepository.ExistsAsync(x => (x.Name == request.Name || x.Slug == request.Slug) && x.Id != request.Id))
            return ProductErrors.Duplicate;

        var updatedProduct = mapper.Map(request, product);

        if (request.Image is not null)
            updatedProduct.Image = await imageUploader.UploadImageAsync(request.Image, "products");
        else
            updatedProduct.Image = product.Image;

        productsRepository.Update(updatedProduct);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("محصول با موفقیت ویرایش شد.");
    }
}
