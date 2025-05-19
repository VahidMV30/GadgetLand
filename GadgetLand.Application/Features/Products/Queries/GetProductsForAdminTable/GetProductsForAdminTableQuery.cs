using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductsForAdminTable;

public record GetProductsForAdminTableQuery() : IRequest<IEnumerable<ProductsForAdminTableResponse>>;
