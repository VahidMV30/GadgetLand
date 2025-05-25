using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductWithImagesById;

public class GetProductWithImagesByIdQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetProductWithImagesByIdQuery, ErrorOr<ProductWithImagesResponse>>
{
    public async Task<ErrorOr<ProductWithImagesResponse>> Handle(GetProductWithImagesByIdQuery query, CancellationToken cancellationToken)
    {
        var productWithImages = await productsRepository.GetProductWithImagesByIdAsync(query.Id);

        if (productWithImages is null) return ProductErrors.NotFound;

        return mapper.Map<ProductWithImagesResponse>(productWithImages);
    }
}
