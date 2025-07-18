﻿namespace GadgetLand.Contracts.Products;

public record PaginatedProductsWithFiltersResponse
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<ProductCardResponse> Products { get; set; } = [];
}
