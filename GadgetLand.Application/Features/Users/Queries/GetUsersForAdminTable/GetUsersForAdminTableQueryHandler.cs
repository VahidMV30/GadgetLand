using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Users;
using MediatR;

namespace GadgetLand.Application.Features.Users.Queries.GetUsersForAdminTable;

public class GetUsersForAdminTableQueryHandler(
    IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetUsersForAdminTableQuery, IEnumerable<UsersForAdminTableResponse>>
{
    public async Task<IEnumerable<UsersForAdminTableResponse>> Handle(GetUsersForAdminTableQuery query, CancellationToken cancellationToken)
    {
        var users = await usersRepository.GetAllUsersAsync();

        return mapper.Map<IEnumerable<UsersForAdminTableResponse>>(users);
    }
}
