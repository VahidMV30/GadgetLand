using AutoMapper;
using GadgetLand.Contracts.Settings;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class SettingMappings : Profile
{
    public SettingMappings()
    {
        CreateMap<Setting, SettingsResponse>();
    }
}
