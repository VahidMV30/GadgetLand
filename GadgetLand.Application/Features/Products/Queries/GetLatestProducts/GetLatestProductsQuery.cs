using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetLatestProducts;

public record GetLatestProductsQuery(int Count) : IRequest<IEnumerable<ProductCardResponse>>;
