using ErrorOr;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductWithImagesById;

public record GetProductWithImagesByIdQuery(int Id) : IRequest<ErrorOr<ProductWithImagesResponse>>;
