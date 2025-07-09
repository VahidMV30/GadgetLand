namespace GadgetLand.Contracts.Orders;

public record ChangeOrderStatusRequest(int OrderId, int OrderStatus);
