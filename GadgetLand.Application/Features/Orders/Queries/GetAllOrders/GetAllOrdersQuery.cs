using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<IEnumerable<OrderResponse>>;
