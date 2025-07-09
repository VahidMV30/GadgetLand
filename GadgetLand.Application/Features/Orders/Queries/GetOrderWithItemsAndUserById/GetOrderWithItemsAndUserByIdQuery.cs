using ErrorOr;
using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetOrderWithItemsAndUserById;

public record GetOrderWithItemsAndUserByIdQuery(int UserId) : IRequest<ErrorOr<OrderWithItemsAndUserResponse>>;
