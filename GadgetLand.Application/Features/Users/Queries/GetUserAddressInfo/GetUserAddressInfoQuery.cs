using ErrorOr;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUserAddressInfo;

public record GetUserAddressInfoQuery() : IRequest<ErrorOr<UserAddressInfoResponse>>;
