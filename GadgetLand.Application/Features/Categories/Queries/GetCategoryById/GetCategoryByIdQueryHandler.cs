using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Categories;
using MediatR;

namespace GadgetLand.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ErrorOr<CategoryResponse>>
{
    public async Task<ErrorOr<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(query.Id);

        if (category is null) return CategoryErrors.NotFound;

        return mapper.Map<CategoryResponse>(category);
    }
}
