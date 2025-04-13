using ErrorOr;
using GadgetLand.Contracts.Auth;
using MediatR;

namespace GadgetLand.Application.Features.Auth.Queries.GetProfile;

public record GetProfileQuery() : IRequest<ErrorOr<AuthResponse>>;
