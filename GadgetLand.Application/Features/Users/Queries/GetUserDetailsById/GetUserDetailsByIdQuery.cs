using ErrorOr;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUserDetailsById;

public record GetUserDetailsByIdQuery() : IRequest<ErrorOr<UserDetailsResponse>>;
