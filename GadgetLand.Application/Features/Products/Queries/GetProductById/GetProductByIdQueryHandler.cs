using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using MediatR;

namespace GadgetLand.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetProductByIdAsync(query.Id);

        if (product is null) return ProductErrors.NotFound;

        return mapper.Map<ProductResponse>(product);
    }
}
