using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetTopSellingProducts;

public record GetTopSellingProductsQuery(int Count) : IRequest<IEnumerable<ProductCardResponse>>;
