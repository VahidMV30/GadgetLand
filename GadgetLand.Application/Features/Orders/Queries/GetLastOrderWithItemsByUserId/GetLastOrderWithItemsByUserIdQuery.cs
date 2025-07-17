using ErrorOr;
using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetLastOrderWithItemsByUserId;

public record GetLastOrderWithItemsByUserIdQuery : IRequest<ErrorOr<OrderWithItemsResponse>>;
