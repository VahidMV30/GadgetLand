using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetLatestProducts;

public class GetLatestProductsQueryHandler(
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetLatestProductsQuery, IEnumerable<ProductCardResponse>>
{
    public async Task<IEnumerable<ProductCardResponse>> Handle(GetLatestProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetLatestProductsAsync(query.Count);

        return mapper.Map<IEnumerable<ProductCardResponse>>(products);
    }
}
