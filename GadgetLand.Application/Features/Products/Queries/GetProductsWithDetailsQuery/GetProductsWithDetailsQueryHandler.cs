using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductsWithDetailsQuery;

public class GetProductsWithDetailsQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetProductsWithDetailsQuery, IEnumerable<ProductDetailsResponse>>
{
    public async Task<IEnumerable<ProductDetailsResponse>> Handle(GetProductsWithDetailsQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetProductsWithDetailsAsync();

        return mapper.Map<IEnumerable<ProductDetailsResponse>>(products);
    }
}
