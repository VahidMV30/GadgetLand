using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUserAddressInfo;

public class GetUserAddressInfoQueryHandler(
    ISecurityService securityService,
    IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetUserAddressInfoQuery, ErrorOr<UserAddressInfoResponse>>
{
    public async Task<ErrorOr<UserAddressInfoResponse>> Handle(GetUserAddressInfoQuery query, CancellationToken cancellationToken)
    {
        var userId = Convert.ToInt32(securityService.GetUserIdFromToken());

        var user = await usersRepository.GetUserAddressInfoByIdAsync(userId);

        if (user is null) return UserErrors.NotFound;

        return mapper.Map<UserAddressInfoResponse>(user);
    }
}
