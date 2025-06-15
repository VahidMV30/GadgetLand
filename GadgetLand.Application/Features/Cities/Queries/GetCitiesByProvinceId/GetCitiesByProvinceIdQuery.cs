using GadgetLand.Contracts.Cities;
using MediatR;

namespace GadgetLand.Application.Features.Cities.Queries.GetCitiesByProvinceId;

public record GetCitiesByProvinceIdQuery(int ProvinceId) : IRequest<IEnumerable<CityResponse>>;
