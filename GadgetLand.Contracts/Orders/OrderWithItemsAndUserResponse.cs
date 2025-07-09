using GadgetLand.Contracts.OrderItems;
using GadgetLand.Contracts.Users;

namespace GadgetLand.Contracts.Orders;

public class OrderWithItemsAndUserResponse
{
    public UserDetailsResponse User { get; set; } = null!;
    public OrderDetailsResponse Order { get; set; } = null!;
    public List<OrderItemResponse> OrderItems { get; set; } = [];
}
