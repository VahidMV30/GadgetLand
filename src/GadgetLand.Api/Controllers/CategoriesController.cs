using GadgetLand.Application.Features.Categories.Commands.CreateCategory;
using GadgetLand.Application.Features.Categories.Commands.UpdateCategory;
using GadgetLand.Application.Features.Categories.Queries.GetAllCategoriesQuery;
using GadgetLand.Contracts.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class CategoriesController(IMediator mediator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllCategoriesQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name, request.Slug, request.Image);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(request.Id, request.Name, request.Slug, request.Image);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
