using Microsoft.AspNetCore.Http;

namespace GadgetLand.Contracts.Categories;

public record UpdateCategoryRequest(int Id, string Name, string Slug, IFormFile Image);
