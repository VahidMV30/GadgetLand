using ErrorOr;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductDetailsBySlug;

public record GetProductDetailsBySlugQuery(string slug) : IRequest<ErrorOr<ProductDetailsResponse>>;
