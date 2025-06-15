using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Cities;
using MediatR;

namespace GadgetLand.Application.Features.Cities.Queries.GetCitiesByProvinceId;

public class GetCitiesByProvinceIdQueryHandler(
    ICitiesRepository citiesRepository,
    IMapper mapper) : IRequestHandler<GetCitiesByProvinceIdQuery, IEnumerable<CityResponse>>
{
    public async Task<IEnumerable<CityResponse>> Handle(GetCitiesByProvinceIdQuery query, CancellationToken cancellationToken)
    {
        var cities = await citiesRepository.GetCitiesByProvinceIdAsync(query.ProvinceId);

        return mapper.Map<IEnumerable<CityResponse>>(cities);
    }
}
