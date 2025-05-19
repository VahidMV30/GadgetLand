using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductsForAdminTable;

public class GetProductsForAdminTableQueryHandler(
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetProductsForAdminTableQuery, IEnumerable<ProductsForAdminTableResponse>>
{
    public async Task<IEnumerable<ProductsForAdminTableResponse>> Handle(GetProductsForAdminTableQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetProductsForAdminTableAsync();

        return mapper.Map<IEnumerable<ProductsForAdminTableResponse>>(products);
    }
}
