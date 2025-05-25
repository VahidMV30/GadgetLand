using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandHandler(
    IBrandsRepository brandsRepository,
    IMapper mapper,
    IImageUploader imageUploader,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateBrandCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        request = request with { Name = request.Name.NormalizeSpaces(), Slug = request.Slug.Slugify() };

        var brand = await brandsRepository.GetByIdAsync(request.Id);

        if (brand is null) return BrandErrors.NotFound;

        if (await brandsRepository.ExistsAsync(x => (x.Name == request.Name || x.Slug == request.Slug) && x.Id != request.Id))
            return BrandErrors.Duplicate;
        var updatedBrand = mapper.Map(request, brand);

        if (request.Image is not null)
        {
            imageUploader.DeleteImage(brand.Image, "brands");
            updatedBrand.Image = await imageUploader.UploadImageAsync(request.Image, "brands");
        }
        else
        {
            updatedBrand.Image = brand.Image;
        }

        brandsRepository.Update(updatedBrand);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("برند با موفقیت ویرایش شد.");
    }
}
