using GadgetLand.Application.Features.Orders.Commands.ChangeOrderStatusById;
using GadgetLand.Application.Features.Orders.Queries.GetAllOrders;
using GadgetLand.Application.Features.Orders.Queries.GetLastOrderWithItemsByUserId;
using GadgetLand.Application.Features.Orders.Queries.GetOrdersByUserId;
using GadgetLand.Application.Features.Orders.Queries.GetOrderWithItemsAndUserById;
using GadgetLand.Application.Features.Orders.Queries.GetOrderWithItemsById;
using GadgetLand.Contracts.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class OrdersController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
        var query = new GetAllOrdersQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("order-with-items-and-user/{orderId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetOrderWithItemsAndUser([FromRoute] int orderId)
    {
        var query = new GetOrderWithItemsAndUserByIdQuery(orderId);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeOrderStatusById([FromBody] ChangeOrderStatusRequest request)
    {
        var command = new ChangeOrderStatusByIdCommand(request.OrderId, request.OrderStatus);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpGet("orders-by-userId")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetOrdersByUserId()
    {
        var query = new GetOrdersByUserIdQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("order-by-orderId/{orderId:int}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetOrderWithItemsById([FromRoute] int orderId)
    {
        var query = new GetOrderWithItemsByIdQuery(orderId);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("last-order-with-items-userId")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetLastOrderWithItemsByUserId()
    {
        var query = new GetLastOrderWithItemsByUserIdQuery();

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }
}
