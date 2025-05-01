using ErrorOr;
using GadgetLand.Contracts.Auth;
using MediatR;

namespace GadgetLand.Application.Features.Auth.Commands.Register;

public record RegisterCommand(string FullName, string Email, string Password, string ConfirmPassword) : IRequest<ErrorOr<RegisterResponse>>;
