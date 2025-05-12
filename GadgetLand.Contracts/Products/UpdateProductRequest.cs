using Microsoft.AspNetCore.Http;

namespace GadgetLand.Contracts.Products;

public record UpdateProductRequest(
    int Id,
    int CategoryId,
    int BrandId,
    string Name,
    string Slug,
    IFormFile Image,
    string Price,
    string? DiscountPrice,
    int? QuantityInStock,
    string Description);
