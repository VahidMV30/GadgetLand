using AutoMapper;
using GadgetLand.Application.Features.Auth.Commands.Login;
using GadgetLand.Application.Features.Auth.Commands.Register;
using GadgetLand.Contracts.Auth;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class AuthMappings : Profile
{
    public AuthMappings()
    {
        CreateMap<RegisterCommand, User>();

        CreateMap<User, RegisterResponse>()
            .ForMember(dest => dest.AuthResponse,
                opt => opt.MapFrom(src => new AuthResponse
                { Id = src.Id, Role = src.Role.Name, FullName = src.FullName, Email = src.Email }))
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<LoginCommand, User>();

        CreateMap<User, LoginResponse>()
            .ForMember(dest => dest.AuthResponse,
                opt => opt.MapFrom(src => new AuthResponse
                { Id = src.Id, Role = src.Role.Name, FullName = src.FullName, Email = src.Email }))
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<User, AuthResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
    }
}
