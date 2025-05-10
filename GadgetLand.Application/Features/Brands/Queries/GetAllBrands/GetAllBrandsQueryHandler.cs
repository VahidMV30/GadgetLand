using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Brands;
using MediatR;

namespace GadgetLand.Application.Features.Brands.Queries.GetAllBrands;

public class GetAllBrandsQueryHandler(IBrandsRepository brandsRepository, IMapper mapper) : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandResponse>>
{
    public async Task<IEnumerable<BrandResponse>> Handle(GetAllBrandsQuery query, CancellationToken cancellationToken)
    {
        var brands = await brandsRepository.GetAllAsync();

        return mapper.Map<IEnumerable<BrandResponse>>(brands);
    }
}
