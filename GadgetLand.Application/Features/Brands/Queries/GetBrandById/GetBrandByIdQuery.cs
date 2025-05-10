using ErrorOr;
using GadgetLand.Contracts.Brands;
using MediatR;

namespace GadgetLand.Application.Features.Brands.Queries.GetBrandById;

public record GetBrandByIdQuery(int Id) : IRequest<ErrorOr<BrandResponse>>;
