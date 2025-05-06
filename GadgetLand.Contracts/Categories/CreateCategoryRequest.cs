using Microsoft.AspNetCore.Http;

namespace GadgetLand.Contracts.Categories;

public record CreateCategoryRequest(string Name, string Slug, IFormFile Image);
