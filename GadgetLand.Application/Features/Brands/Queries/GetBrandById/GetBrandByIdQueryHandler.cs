using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Brands;
using MediatR;

namespace GadgetLand.Application.Features.Brands.Queries.GetBrandById;

public class GetBrandByIdQueryHandler(IBrandsRepository brandsRepository, IMapper mapper) : IRequestHandler<GetBrandByIdQuery, ErrorOr<BrandResponse>>
{
    public async Task<ErrorOr<BrandResponse>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
    {
        var brand = await brandsRepository.GetByIdAsync(query.Id);

        if (brand is null) return BrandErrors.NotFound;

        return mapper.Map<BrandResponse>(brand);
    }
}
