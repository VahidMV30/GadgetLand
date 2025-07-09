using GadgetLand.Contracts.Users;

namespace GadgetLand.Contracts.Orders;

public record OrderResponse
{
    public int Id { get; set; }

    public UserInOrderResponse User { get; set; } = null!;
    public string TotalPayableAmount { get; set; } = string.Empty;
    public long RefId { get; set; }
    public string OrderDate { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
}
