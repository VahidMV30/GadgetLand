namespace GadgetLand.Contracts.Products;

public record ProductsWithFiltersRequest(
    string? CategorySlug = null,
    string? BrandSlug = null,
    bool OnlyDiscounted = false,
    ProductSortOrder SortOrder = ProductSortOrder.Latest,
    int PageIndex = 1,
    int PageSize = 10);
