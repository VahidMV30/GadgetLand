using GadgetLand.Application.Features.Users.Commands.UpdateUserAddressInfo;
using GadgetLand.Application.Features.Users.Commands.UpdateUserInfo;
using GadgetLand.Application.Features.Users.Queries.GetUserAddressInfo;
using GadgetLand.Application.Features.Users.Queries.GetUserDetailsById;
using GadgetLand.Application.Features.Users.Queries.GetUserDetailsWithOrders;
using GadgetLand.Application.Features.Users.Queries.GetUsersForAdminTable;
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

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsersForAdminTable()
    {
        var query = new GetUsersForAdminTableQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("user-details-with-orders/{userId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserDetailsWithOrders([FromRoute] int userId)
    {
        var query = new GetUserDetailsWithOrdersQuery(userId);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("user-details")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetUserDetailsById()
    {
        var query = new GetUserDetailsByIdQuery();

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPut]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequest request)
    {
        var command = new UpdateUserInfoCommand(request.CityId, request.FullName, request.Mobile, request.PostalCode, request.Address);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
