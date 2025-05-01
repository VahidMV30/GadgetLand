using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Auth;
using MediatR;

namespace GadgetLand.Application.Features.Auth.Queries.GetProfile;

public class GetProfileQueryHandler(ISecurityService securityService, IUsersRepository usersRepository, IMapper mapper) : IRequestHandler<GetProfileQuery, ErrorOr<AuthResponse>>
{
    public async Task<ErrorOr<AuthResponse>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var email = securityService.GetEmailFromToken();

        var user = await usersRepository.GetByEmailAsync(email);

        return mapper.Map<AuthResponse>(user);
    }
}
