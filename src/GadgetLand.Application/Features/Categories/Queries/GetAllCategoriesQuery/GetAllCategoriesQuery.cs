using GadgetLand.Contracts.Categories;
using MediatR;

namespace GadgetLand.Application.Features.Categories.Queries.GetAllCategoriesQuery;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryResponse>>;
