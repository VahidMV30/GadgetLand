using GadgetLand.Contracts.OrderItems;

namespace GadgetLand.Contracts.Orders;

public record OrderWithItemsResponse
{
    public OrderDetailsResponse Order { get; set; } = null!;
    public List<OrderItemResponse> OrderItems { get; set; } = [];
}
