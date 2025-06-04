using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductDetailsBySlug;

public class GetProductDetailsBySlugQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetProductDetailsBySlugQuery, ErrorOr<ProductDetailsResponse>>
{
    public async Task<ErrorOr<ProductDetailsResponse>> Handle(GetProductDetailsBySlugQuery query, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetProductDetailsBySlugAsync(query.slug);

        if (product is null) return ProductErrors.NotFound;

        return mapper.Map<ProductDetailsResponse>(product);
    }
}
