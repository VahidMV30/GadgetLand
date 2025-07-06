namespace GadgetLand.Contracts.Products;

public record ProductCardResponse
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string? DiscountPrice { get; set; } = string.Empty;
    public int? DiscountPercent { get; set; }
}
