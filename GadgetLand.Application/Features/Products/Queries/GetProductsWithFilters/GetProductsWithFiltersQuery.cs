using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductsWithFilters;

public record GetProductsWithFiltersQuery(string? CategorySlug, string? BrandSlug, bool OnlyDiscounted, ProductSortOrder SortOrder, int PageIndex, int PageSize) :
    IRequest<PaginatedProductsWithFiltersResponse>;
