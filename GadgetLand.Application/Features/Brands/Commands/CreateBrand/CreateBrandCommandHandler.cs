using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using GadgetLand.Domain.Entities;
using MediatR;

namespace GadgetLand.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler(
    IBrandsRepository brandsRepository,
    IMapper mapper,
    IImageUploader imageUploader,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateBrandCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        request = request with { Name = request.Name.NormalizeSpaces(), Slug = request.Slug.Slugify() };

        var existingBrand = await brandsRepository.ExistsAsync(x => x.Name == request.Name || x.Slug == request.Slug);

        if (existingBrand is true) return BrandErrors.Duplicate;

        var brand = mapper.Map<Brand>(request);
        brand.Image = await imageUploader.UploadImageAsync(request.Image, "brands");

        await brandsRepository.CreateAsync(brand);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("برند با موفقیت ایجاد شد.");
    }
}
