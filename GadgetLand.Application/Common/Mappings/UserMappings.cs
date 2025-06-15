using AutoMapper;
using GadgetLand.Application.Features.Users.Commands.UpdateUserAddressInfo;
using GadgetLand.Contracts.Users;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateMap<User, UserAddressInfoResponse>()
            .ForMember(dest => dest.ProvinceId, opt =>
                opt.MapFrom(src => src.City!.ProvinceId))
            .ForMember(dest => dest.ProvinceName, opt =>
                opt.MapFrom(src => src.City!.Province.Name))
            .ForMember(dest => dest.CityId, opt =>
                opt.MapFrom(src => src.CityId));

        CreateMap<UpdateUserAddressInfoCommand, User>();
    }
}
