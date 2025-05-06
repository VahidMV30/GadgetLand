using AutoMapper;
using GadgetLand.Application.Features.Categories.Commands.CreateCategory;
using GadgetLand.Application.Features.Categories.Commands.UpdateCategory;
using GadgetLand.Contracts.Categories;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<Category, CategoryResponse>();

        CreateMap<CreateCategoryCommand, Category>();

        CreateMap<UpdateCategoryCommand, Category>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
    }
}
