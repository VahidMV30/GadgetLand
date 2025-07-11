using GadgetLand.Contracts.OrderItems;
using GadgetLand.Contracts.Users;

namespace GadgetLand.Contracts.Orders;

public record OrderWithItemsForUserPanelResponse
{
    public UserDetailsResponse User { get; set; } = null!;
    public OrderForUserPanelResponse Order { get; set; } = null!;
    public IEnumerable<OrderItemResponse> OrderItems { get; set; } = [];
}
