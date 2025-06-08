using GadgetLand.Application.Features.Products.Commands.CreateProduct;
using GadgetLand.Application.Features.Products.Commands.ModifyProductImages;
using GadgetLand.Application.Features.Products.Commands.UpdateProduct;
using GadgetLand.Application.Features.Products.Queries.GetProductById;
using GadgetLand.Application.Features.Products.Queries.GetProductDetailsBySlug;
using GadgetLand.Application.Features.Products.Queries.GetProductsForAdminTable;
using GadgetLand.Application.Features.Products.Queries.GetProductsWithFilters;
using GadgetLand.Application.Features.Products.Queries.GetProductWithImagesById;
using GadgetLand.Contracts.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ApiController
{
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var query = new GetProductByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetProductsForAdminTable()
    {
        var query = new GetProductsForAdminTableQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("product-with-images/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetProductWithImagesById(int id)
    {
        var query = new GetProductWithImagesByIdQuery(id);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("products-with-filters")]
    public async Task<IActionResult> GetProductsWithFilters([FromQuery] ProductsWithFiltersRequest request)
    {
        var query = new GetProductsWithFiltersQuery(request.CategorySlug, request.BrandSlug, request.OnlyDiscounted, request.SortOrder, request.PageIndex, request.PageSize);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("product-details/{slug}")]
    public async Task<IActionResult> GetProductDetailsBySlug(string slug)
    {
        var query = new GetProductDetailsBySlugQuery(slug);

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
    {
        var command = new CreateProductCommand(request.CategoryId, request.BrandId, request.Name, request.Slug,
            request.Image, request.Price, request.DiscountPrice, request.QuantityInStock, request.Description);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest request)
    {
        var command = new UpdateProductCommand(request.Id, request.CategoryId, request.BrandId, request.Name,
            request.Slug, request.Image, request.Price, request.DiscountPrice, request.QuantityInStock, request.Description);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }

    [HttpPost("modify-product-images")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ModifyProductImages([FromForm] ModifyProductImagesRequest request)
    {
        var command = new ModifyProductImagesCommand(request.Id, request.ImagesToRemove, request.NewImages);

        var result = await mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
