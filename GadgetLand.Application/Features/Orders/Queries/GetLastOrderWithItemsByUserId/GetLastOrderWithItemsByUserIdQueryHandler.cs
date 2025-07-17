using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.OrderItems;
using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetLastOrderWithItemsByUserId;

public class GetLastOrderWithItemsByUserIdQueryHandler(
    IOrdersRepository ordersRepository,
    ISecurityService securityService,
    IMapper mapper) : IRequestHandler<GetLastOrderWithItemsByUserIdQuery, ErrorOr<OrderWithItemsResponse>>
{
    public async Task<ErrorOr<OrderWithItemsResponse>> Handle(GetLastOrderWithItemsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetLastOrderWithItemsByUserIdAsync(int.Parse(securityService.GetUserIdFromToken()));

        if (order is null) return OrderErrors.LastOrderNotFound;

        return new OrderWithItemsResponse
        {
            Order = mapper.Map<OrderDetailsResponse>(order),
            OrderItems = mapper.Map<List<OrderItemResponse>>(order.OrderItems)
        };
    }
}
