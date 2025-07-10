using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Features.Users.Commands.UpdateUserAddressInfo;
using GadgetLand.Application.Features.Users.Commands.UpdateUserInfo;
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

        CreateMap<User, UsersForAdminTableResponse>()
            .ForMember(dest => dest.Mobile, opt =>
                opt.MapFrom(src => !string.IsNullOrEmpty(src.Mobile) ? src.Mobile : "-"))
            .ForMember(dest => dest.Province, opt =>
                opt.MapFrom(src => src.City != null ? src.City.Province.Name : "-"))
            .ForMember(dest => dest.City, opt =>
                opt.MapFrom(src => src.City != null ? src.City.Name : "-"))
            .ForMember(dest => dest.RegisterDate, opt =>
                opt.MapFrom(src => src.RegisterDate.ToPersianDateString()));

        CreateMap<User, UserDetailsResponse>()
            .ForMember(dest => dest.ProvinceId, opt =>
                opt.MapFrom(src => src.City != null ? src.City.Province.Id : (int?)null))
            .ForMember(dest => dest.Province, opt =>
                opt.MapFrom(src => src.City != null ? src.City.Province.Name : ""))
            .ForMember(dest => dest.City, opt =>
                opt.MapFrom(src => src.City != null ? src.City.Name : ""))
            .ForMember(dest => dest.RegisterDate, opt =>
                opt.MapFrom(src => src.RegisterDate.ToPersianDateString()));

        CreateMap<User, UserInOrderResponse>();

        CreateMap<UpdateUserInfoCommand, User>();
    }
}
