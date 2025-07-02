namespace GadgetLand.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public long UnitPrice { get; set; } = 0;
    public long UnitDiscount { get; set; } = 0;
    public long TotalDiscountAmount { get; set; } = 0;
    public long SubtotalAmount { get; set; } = 0;
    public long TotalAmount { get; set; } = 0;
}
