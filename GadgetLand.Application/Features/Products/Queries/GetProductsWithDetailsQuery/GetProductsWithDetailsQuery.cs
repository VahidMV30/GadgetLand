using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductsWithDetailsQuery;

public record GetProductsWithDetailsQuery() : IRequest<IEnumerable<ProductDetailsResponse>>;
