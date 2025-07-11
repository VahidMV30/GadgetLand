using ErrorOr;
using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetOrderWithItemsById;

public record GetOrderWithItemsByIdQuery(int OrderId) : IRequest<ErrorOr<OrderWithItemsForUserPanelResponse>>;
