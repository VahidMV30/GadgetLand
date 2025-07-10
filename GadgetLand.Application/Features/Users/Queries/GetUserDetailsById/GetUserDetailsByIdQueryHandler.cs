using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUserDetailsById;

public class GetUserDetailsByIdQueryHandler(
    IUsersRepository usersRepository,
    ISecurityService securityService,
    IMapper mapper) : IRequestHandler<GetUserDetailsByIdQuery, ErrorOr<UserDetailsResponse>>
{
    public async Task<ErrorOr<UserDetailsResponse>> Handle(GetUserDetailsByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserDetailsByIdAsync(int.Parse(securityService.GetUserIdFromToken()));

        if (user == null) return UserErrors.NotFound;

        return mapper.Map<UserDetailsResponse>(user);
    }
}
