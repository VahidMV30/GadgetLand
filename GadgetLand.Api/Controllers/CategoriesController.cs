using GadgetLand.Application.Features.Categories.Commands.CreateCategory;
using GadgetLand.Application.Features.Categories.Commands.UpdateCategory;
using GadgetLand.Application.Features.Categories.Queries.GetAllCategories;
using GadgetLand.Application.Features.Categories.Queries.GetCategoryById;
using GadgetLand.Contracts.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetCategoryByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromForm] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name, request.Slug, request.Image);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromForm] UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(request.Id, request.Name, request.Slug, request.Image);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
