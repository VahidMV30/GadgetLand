using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Provinces;
using MediatR;

namespace GadgetLand.Application.Features.Provinces.Queries.GetAllProvinces;

public class GetAllProvincesQueryHandler(IProvincesRepository provincesRepository, IMapper mapper) : IRequestHandler<GetAllProvincesQuery, IEnumerable<ProvinceResponse>>
{
    public async Task<IEnumerable<ProvinceResponse>> Handle(GetAllProvincesQuery query, CancellationToken cancellationToken)
    {
        var provinces = await provincesRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ProvinceResponse>>(provinces);
    }
}
