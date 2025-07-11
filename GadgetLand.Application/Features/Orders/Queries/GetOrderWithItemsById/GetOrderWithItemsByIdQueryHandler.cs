using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.OrderItems;
using GadgetLand.Contracts.Orders;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetOrderWithItemsById;

public class GetOrderWithItemsByIdQueryHandler(
    IOrdersRepository ordersRepository,
    ISecurityService securityService,
    IMapper mapper) : IRequestHandler<GetOrderWithItemsByIdQuery, ErrorOr<OrderWithItemsForUserPanelResponse>>
{
    public async Task<ErrorOr<OrderWithItemsForUserPanelResponse>> Handle(GetOrderWithItemsByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetOrderWithItemsByIdAsync(query.OrderId, int.Parse(securityService.GetUserIdFromToken()));

        if (order is null) return OrderErrors.NotFound;

        return new OrderWithItemsForUserPanelResponse
        {
            User = mapper.Map<UserDetailsResponse>(order.User),
            Order = mapper.Map<OrderForUserPanelResponse>(order),
            OrderItems = mapper.Map<IEnumerable<OrderItemResponse>>(order.OrderItems)
        };
    }
}
