using GadgetLand.Application.Features.Reviews.Commands;
using GadgetLand.Application.Features.Reviews.Queries.HasUserReviewed;
using GadgetLand.Contracts.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class ReviewsController(IMediator mediator) : ApiController
{
    [HttpGet("has-user-reviewed/{productId:int}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> HasUserReviewed(int productId)
    {
        var query = new HasUserReviewedQuery(productId);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Create(CreateReviewRequest request)
    {
        var command = new CreateReviewCommand(request.ProductId, request.Rating, request.Comment);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
