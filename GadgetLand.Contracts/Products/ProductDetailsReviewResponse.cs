namespace GadgetLand.Contracts.Products;

public record ProductDetailsReviewResponse
{
    public string FullName { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
}
