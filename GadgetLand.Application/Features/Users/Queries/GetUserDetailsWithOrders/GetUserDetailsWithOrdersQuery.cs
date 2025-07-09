using ErrorOr;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUserDetailsWithOrders;

public record GetUserDetailsWithOrdersQuery(int UserId) : IRequest<ErrorOr<UserDetailsWithOrdersResponse>>;
