using GadgetLand.Contracts.Provinces;
using MediatR;

namespace GadgetLand.Application.Features.Provinces.Queries.GetAllProvinces;

public class GetAllProvincesQuery() : IRequest<IEnumerable<ProvinceResponse>>;
