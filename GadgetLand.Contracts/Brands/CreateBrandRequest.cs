using Microsoft.AspNetCore.Http;

namespace GadgetLand.Contracts.Brands;

public record CreateBrandRequest(string Name, string Slug, IFormFile Image);
