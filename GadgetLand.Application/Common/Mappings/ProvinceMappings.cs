using AutoMapper;
using GadgetLand.Contracts.Provinces;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class ProvinceMappings : Profile
{
    public ProvinceMappings()
    {
        CreateMap<Province, ProvinceResponse>();
    }
}
