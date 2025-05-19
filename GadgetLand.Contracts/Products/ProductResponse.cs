namespace GadgetLand.Contracts.Products;

public record ProductResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string DiscountPrice { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public string Description { get; set; } = string.Empty;
}
