using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetDiscountedProducts;

public class GetDiscountedProductsQueryHandler(
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetDiscountedProductsQuery, IEnumerable<ProductCardResponse>>
{
    public async Task<IEnumerable<ProductCardResponse>> Handle(GetDiscountedProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetDiscountedProductsAsync(query.Count);

        return mapper.Map<IEnumerable<ProductCardResponse>>(products);
    }
}
