using ErrorOr;
using GadgetLand.Contracts.Categories;
using MediatR;

namespace GadgetLand.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(int Id) : IRequest<ErrorOr<CategoryResponse>>;
