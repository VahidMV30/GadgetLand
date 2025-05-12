using GadgetLand.Application.Features.Products.Commands.CreateProduct;
using GadgetLand.Application.Features.Products.Commands.UpdateProduct;
using GadgetLand.Application.Features.Products.Queries.GetProductById;
using GadgetLand.Application.Features.Products.Queries.GetProductsWithDetailsQuery;
using GadgetLand.Contracts.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetProductsWithDetails()
    {
        var query = new GetProductsWithDetailsQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetProductByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
    {
        var command = new CreateProductCommand(request.CategoryId, request.BrandId, request.Name, request.Slug,
            request.Image, request.Price, request.DiscountPrice, request.QuantityInStock, request.Description);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromForm] UpdateProductRequest request)
    {
        var command = new UpdateProductCommand(request.Id, request.CategoryId, request.BrandId, request.Name,
            request.Slug, request.Image, request.Price, request.DiscountPrice, request.QuantityInStock, request.Description);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
