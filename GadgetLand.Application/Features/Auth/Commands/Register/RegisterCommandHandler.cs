using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Auth;
using GadgetLand.Domain.Entities;
using MediatR;

namespace GadgetLand.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUsersRepository usersRepository,
    IRolesRepository rolesRepository,
    IMapper mapper,
    ISecurityService securityService,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, ErrorOr<RegisterResponse>>
{
    public async Task<ErrorOr<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        request = request with { FullName = request.FullName.NormalizeSpaces() };

        var exitingUser = await usersRepository.GetByEmailAsync(request.Email);

        if (exitingUser is not null) return AuthErrors.Duplicate;

        var userRole = await rolesRepository.GetByNameAsync("User");

        var newUser = mapper.Map<User>(request);
        newUser.RoleId = userRole!.Id;
        newUser.Password = securityService.HashPassword(request.Password);

        await usersRepository.CreateAsync(newUser);
        await unitOfWork.CommitChangesAsync();

        var createdUser = await usersRepository.GetByEmailAsync(newUser.Email);

        var registerResponse = mapper.Map<RegisterResponse>(createdUser);
        registerResponse = registerResponse with { Token = securityService.GenerateToken(createdUser!) };

        return registerResponse;
    }
}
