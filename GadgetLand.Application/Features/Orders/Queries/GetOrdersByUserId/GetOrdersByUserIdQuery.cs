using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetOrdersByUserId;

public record GetOrdersByUserIdQuery() : IRequest<IEnumerable<OrderForUserPanelResponse>>;
