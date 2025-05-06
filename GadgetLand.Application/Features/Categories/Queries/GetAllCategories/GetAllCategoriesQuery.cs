using GadgetLand.Contracts.Categories;
using MediatR;

namespace GadgetLand.Application.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>;
