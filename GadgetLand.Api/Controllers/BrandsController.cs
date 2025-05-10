using GadgetLand.Application.Features.Brands.Commands.CreateBrand;
using GadgetLand.Application.Features.Brands.Commands.UpdateBrand;
using GadgetLand.Application.Features.Brands.Queries.GetAllBrands;
using GadgetLand.Application.Features.Brands.Queries.GetBrandById;
using GadgetLand.Contracts.Brands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class BrandsController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllBrandsQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetBrandByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromForm] CreateBrandRequest request)
    {
        var command = new CreateBrandCommand(request.Name, request.Slug, request.Image);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromForm] UpdateBrandRequest request)
    {
        var command = new UpdateBrandCommand(request.Id, request.Name, request.Slug, request.Image);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
