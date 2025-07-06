using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetDiscountedProducts;

public record GetDiscountedProductsQuery(int Count) : IRequest<IEnumerable<ProductCardResponse>>;
