using GadgetLand.Application.Features.Payments.Commands.CreatePayment;
using GadgetLand.Application.Features.Payments.Queries.VerifyPayment;
using GadgetLand.Contracts.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class PaymentsController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreatePayment(List<CartItem> cartItems)
    {
        var command = new CreatePaymentCommand(cartItems);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpGet]
    public async Task<IActionResult> VerifyPayment([FromQuery] VerifyPaymentRequest request)
    {
        var query = new VerifyPaymentQuery(request.Status, request.Authority);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }
}
