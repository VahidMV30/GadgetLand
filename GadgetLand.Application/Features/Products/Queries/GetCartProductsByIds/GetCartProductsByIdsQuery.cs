using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetCartProductsByIds;

public record GetCartProductsByIdsQuery(List<int> Ids) : IRequest<IEnumerable<CartProductResponse>>;
