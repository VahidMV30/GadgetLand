namespace GadgetLand.Contracts.Products;

public record ProductDetailsResponse
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategorySlug { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string BrandSlug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string DiscountPrice { get; set; } = string.Empty;
    public int? DiscountPercent { get; set; }
    public int QuantityInStock { get; set; }
    public string Description { get; set; } = string.Empty;

    public double AverageRating { get; set; }
    public int TotalReviewsCount { get; set; }

    public IEnumerable<string> ProductImages { get; set; } = [];
    public IEnumerable<ProductDetailsReviewResponse> Reviews { get; set; } = [];
}
