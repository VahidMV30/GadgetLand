using ErrorOr;
using GadgetLand.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GadgetLand.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    int CategoryId,
    int BrandId,
    string Name,
    string Slug,
    IFormFile Image,
    string Price,
    string? DiscountPrice,
    int? QuantityInStock,
    string Description) : IRequest<ErrorOr<OperationResponse>>;
