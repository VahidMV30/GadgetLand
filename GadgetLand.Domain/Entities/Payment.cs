namespace GadgetLand.Domain.Entities;

public class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public long DiscountAmount { get; set; } = 0;
    public long ShippingCost { get; set; } = 0;
    public long SubtotalAmount { get; set; }
    public long TotalPayableAmount { get; set; }
    public string Authority { get; set; } = string.Empty;
    public bool IsPaid { get; set; }
    public long? RefId { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PaymentItem> PaymentItems { get; set; } = [];
}
