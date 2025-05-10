using Microsoft.AspNetCore.Http;

namespace GadgetLand.Contracts.Brands;

public record UpdateBrandRequest(int Id, string Name, string Slug, IFormFile Image);
