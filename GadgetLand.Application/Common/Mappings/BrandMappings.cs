using AutoMapper;
using GadgetLand.Application.Features.Brands.Commands.CreateBrand;
using GadgetLand.Application.Features.Brands.Commands.UpdateBrand;
using GadgetLand.Contracts.Brands;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class BrandMappings : Profile
{
    public BrandMappings()
    {
        CreateMap<Brand, BrandResponse>();

        CreateMap<CreateBrandCommand, Brand>();

        CreateMap<UpdateBrandCommand, Brand>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
    }
}
