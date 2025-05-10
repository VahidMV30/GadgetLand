using GadgetLand.Contracts.Brands;
using MediatR;

namespace GadgetLand.Application.Features.Brands.Queries.GetAllBrands;

public record GetAllBrandsQuery() : IRequest<IEnumerable<BrandResponse>>;
