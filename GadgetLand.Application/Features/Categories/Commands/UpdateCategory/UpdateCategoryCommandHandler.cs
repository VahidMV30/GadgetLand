using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoriesRepository categoriesRepository,
    IMapper mapper,
    IImageUploader imageUploader,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        request = request with { Name = request.Name.NormalizeSpaces(), Slug = request.Slug.Slugify() };

        var category = await categoriesRepository.GetByIdAsync(request.Id);

        if (category is null) return CategoryErrors.NotFound;

        if (await categoriesRepository.ExistsAsync(x => (x.Name == request.Name || x.Slug == request.Slug) && x.Id != request.Id))
            return CategoryErrors.Duplicate;

        var updatedCategory = mapper.Map(request, category);

        if (request.Image is not null)
            updatedCategory.Image = await imageUploader.UploadImageAsync(request.Image, "categories");
        else
            updatedCategory.Image = category.Image;

        categoriesRepository.Update(updatedCategory);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("دسته بندی با موفقیت ویرایش شد.");
    }
}
