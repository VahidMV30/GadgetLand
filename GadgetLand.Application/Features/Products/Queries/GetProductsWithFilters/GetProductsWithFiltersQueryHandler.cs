using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductsWithFilters;

public class GetProductsWithFiltersQueryHandler(
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetProductsWithFiltersQuery, PaginatedProductsWithFiltersResponse>
{
    public async Task<PaginatedProductsWithFiltersResponse> Handle(GetProductsWithFiltersQuery query, CancellationToken cancellationToken)
    {
        var (totalCount, products) = await productsRepository.GetProductsWithFiltersAsync(
            query.CategorySlug, query.BrandSlug, query.OnlyDiscounted, query.SortOrder, query.PageIndex, query.PageSize);

        var mappedProducts = mapper.Map<IEnumerable<ProductCardResponse>>(products);

        return new PaginatedProductsWithFiltersResponse
        {
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize),
            Products = mappedProducts
        };
    }
}
