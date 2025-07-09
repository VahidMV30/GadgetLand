using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.OrderItems;
using GadgetLand.Contracts.Orders;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetOrderWithItemsAndUserById;

public class GetOrderWithItemsAndUserByIdQueryHandler(
    IOrdersRepository ordersRepository,
    IMapper mapper) : IRequestHandler<GetOrderWithItemsAndUserByIdQuery, ErrorOr<OrderWithItemsAndUserResponse>>
{
    public async Task<ErrorOr<OrderWithItemsAndUserResponse>> Handle(GetOrderWithItemsAndUserByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetOrderWithItemsAndUserByIdAsync(query.UserId);

        if (order is null) return OrderErrors.NotFound;

        return new OrderWithItemsAndUserResponse
        {
            User = mapper.Map<UserDetailsResponse>(order.User),
            Order = mapper.Map<OrderDetailsResponse>(order),
            OrderItems = mapper.Map<List<OrderItemResponse>>(order.OrderItems)
        };
    }
}
