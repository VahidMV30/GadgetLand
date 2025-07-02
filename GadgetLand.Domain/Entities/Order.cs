using GadgetLand.Domain.Enums;

namespace GadgetLand.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    public long DiscountAmount { get; set; } = 0;
    public long ShippingCost { get; set; } = 0;
    public long SubtotalAmount { get; set; }
    public long TotalPayableAmount { get; set; }
    public long? RefId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
