using ErrorOr;
using GadgetLand.Contracts.Auth;
using MediatR;

namespace GadgetLand.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<ErrorOr<LoginResponse>>;
