using GadgetLand.Application.Features.Reviews.Commands.CreateReview;
using GadgetLand.Application.Features.Reviews.Commands.DeleteReview;
using GadgetLand.Application.Features.Reviews.Commands.ToggleConformationStatus;
using GadgetLand.Application.Features.Reviews.Queries.GetAllReviews;
using GadgetLand.Application.Features.Reviews.Queries.GetReviewDetailsById;
using GadgetLand.Application.Features.Reviews.Queries.HasUserReviewed;
using GadgetLand.Contracts.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class ReviewsController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllReviews()
    {
        var query = new GetAllReviewsQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("review-details/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetReviewDetailsById(int id)
    {
        var query = new GetReviewDetailsByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

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
    public async Task<IActionResult> CreateReview(CreateReviewRequest request)
    {
        var command = new CreateReviewCommand(request.ProductId, request.Rating, request.Comment);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleConformationStatus(int id)
    {
        var command = new ToggleConformationStatusCommand(id);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPut("delete-review/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var command = new DeleteReviewCommand(id);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
