using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Orders;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(IOrdersRepository ordersRepository, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderResponse>>
{
    public async Task<IEnumerable<OrderResponse>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetAllOrdersAsync();

        return mapper.Map<IEnumerable<OrderResponse>>(order);
    }
}
