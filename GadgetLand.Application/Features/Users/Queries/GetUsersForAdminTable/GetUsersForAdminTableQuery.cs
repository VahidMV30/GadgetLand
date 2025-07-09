using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUsersForAdminTable;

public record GetUsersForAdminTableQuery() : IRequest<IEnumerable<UsersForAdminTableResponse>>;
