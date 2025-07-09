using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Orders;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUserDetailsWithOrders;

public class GetUserDetailsWithOrdersQueryHandler(
    IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetUserDetailsWithOrdersQuery, ErrorOr<UserDetailsWithOrdersResponse>>
{
    public async Task<ErrorOr<UserDetailsWithOrdersResponse>> Handle(GetUserDetailsWithOrdersQuery query, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserDetailsWithOrdersAsync(query.UserId);

        if (user is null) return UserErrors.NotFound;

        return new UserDetailsWithOrdersResponse
        {
            User = mapper.Map<UserDetailsResponse>(user),
            Orders = mapper.Map<List<OrderDetailsResponse>>(user.Orders)
        };
    }
}
