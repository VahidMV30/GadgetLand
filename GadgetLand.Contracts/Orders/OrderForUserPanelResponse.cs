namespace GadgetLand.Contracts.Orders;

public record OrderForUserPanelResponse
{
    public int Id { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public string DiscountAmount { get; set; } = string.Empty;
    public string ShippingCost { get; set; } = string.Empty;
    public string SubtotalAmount { get; set; } = string.Empty;
    public string TotalPayableAmount { get; set; } = string.Empty;
    public long RefId { get; set; }
    public string OrderDate { get; set; } = string.Empty;
}
