using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetTopSellingProducts;

public class GetTopSellingProductsQueryHandler(
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetTopSellingProductsQuery, IEnumerable<ProductCardResponse>>
{
    public async Task<IEnumerable<ProductCardResponse>> Handle(GetTopSellingProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetTopSellingProductsAsync(query.Count);

        return mapper.Map<IEnumerable<ProductCardResponse>>(products);
    }
}
