using ErrorOr;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Users.Commands.UpdateUserInfo;

public record UpdateUserInfoCommand(int? CityId, string FullName, string Mobile, string PostalCode, string Address) : IRequest<ErrorOr<OperationResponse>>;
