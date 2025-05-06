using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Categories;
using MediatR;

namespace GadgetLand.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
{
    public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await categoriesRepository.GetAllAsync();

        return mapper.Map<IEnumerable<CategoryResponse>>(categories);
    }
}
