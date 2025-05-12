using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using GadgetLand.Domain.Entities;
using MediatR;

namespace GadgetLand.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductsRepository productsRepository,
    IMapper mapper,
    IImageUploader imageUploader,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        request = request with { Name = request.Name.NormalizeSpaces(), Slug = request.Slug.Slugify() };

        var existingProduct = await productsRepository.ExistsAsync(x => x.Name == request.Name || x.Slug == request.Slug);

        if (existingProduct) return ProductErrors.Duplicate;

        var product = mapper.Map<Product>(request);
        product.Image = await imageUploader.UploadImageAsync(request.Image, "products");

        await productsRepository.CreateAsync(product);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("محصول با موفقیت ایجاد شد.");
    }
}
