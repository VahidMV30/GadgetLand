namespace GadgetLand.Contracts.Products;

public record CartProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public long Price { get; set; }
    public string StringPrice { get; set; } = string.Empty;
    public long? DiscountPrice { get; set; }
    public string StringDiscountPrice { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
}
