using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Auth;
using MediatR;

namespace GadgetLand.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(IUsersRepository usersRepository, ISecurityService securityService, IMapper mapper) : IRequestHandler<LoginCommand, ErrorOr<LoginResponse>>
{
    public async Task<ErrorOr<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByEmailAsync(request.Email);

        if (user is null) return AuthErrors.InvalidCredentials;

        if (securityService.VerifyPassword(request.Password, user.Password) is false) return AuthErrors.InvalidCredentials;

        var signInResponse = mapper.Map<LoginResponse>(user);
        signInResponse = signInResponse with { Token = securityService.GenerateToken(user) };

        return signInResponse;
    }
}
