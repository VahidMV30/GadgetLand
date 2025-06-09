using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetCartProductsByIds;

public class GetCartProductsByIdsQueryHandler(
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetCartProductsByIdsQuery, IEnumerable<CartProductResponse>>
{
    public async Task<IEnumerable<CartProductResponse>> Handle(GetCartProductsByIdsQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetCartProductsByIdsAsync(query.Ids);

        return mapper.Map<IEnumerable<CartProductResponse>>(products);
    }
}
