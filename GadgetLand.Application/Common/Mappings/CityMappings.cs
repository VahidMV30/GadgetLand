using AutoMapper;
using GadgetLand.Contracts.Cities;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class CityMappings : Profile
{
    public CityMappings()
    {
        CreateMap<City, CityResponse>();
    }
}
