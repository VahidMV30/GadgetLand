using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetOrdersByUserId;

public class GetOrdersByUserIdQueryHandler(
    IOrdersRepository ordersRepository,
    ISecurityService securityService,
    IMapper mapper) : IRequestHandler<GetOrdersByUserIdQuery, IEnumerable<OrderForUserPanelResponse>>
{
    public async Task<IEnumerable<OrderForUserPanelResponse>> Handle(GetOrdersByUserIdQuery query, CancellationToken cancellationToken)
    {
        var orders = await ordersRepository.GetOrdersByUserIdAsync(int.Parse(securityService.GetUserIdFromToken()));

        return mapper.Map<IEnumerable<OrderForUserPanelResponse>>(orders);
    }
}
