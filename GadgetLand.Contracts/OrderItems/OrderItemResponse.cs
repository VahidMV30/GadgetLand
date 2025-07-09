namespace GadgetLand.Contracts.OrderItems;

public record OrderItemResponse
{
    public int Id { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public string ProductSlug { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public string UnitPrice { get; set; } = string.Empty;
    public string UnitDiscount { get; set; } = string.Empty;
    public string TotalDiscountAmount { get; set; } = string.Empty;
    public string SubtotalAmount { get; set; } = string.Empty;
    public string TotalAmount { get; set; } = string.Empty;
}
