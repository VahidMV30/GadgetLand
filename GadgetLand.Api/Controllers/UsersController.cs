using GadgetLand.Application.Features.Users.Commands.UpdateUserAddressInfo;
using GadgetLand.Application.Features.Users.Queries.GetUserAddressInfo;
using GadgetLand.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class UsersController(IMediator mediator) : ApiController
{
    [HttpGet("user-address-info")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetUserAddressInfo()
    {
        var query = new GetUserAddressInfoQuery();

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateUserAddressInfo(UpdateUserAddressInfoRequest request)
    {
        var command = new UpdateUserAddressInfoCommand(request.CityId, request.FullName, request.Mobile, request.PostalCode, request.Address);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
