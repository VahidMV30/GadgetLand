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

namespace GadgetLand.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(
    ICategoriesRepository categoriesRepository,
    IMapper mapper,
    IImageUploader imageUploader,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        request = request with { Name = request.Name.NormalizeSpaces(), Slug = request.Slug.Slugify() };

        var existingCategory = await categoriesRepository.ExistsAsync(x => x.Name == request.Name || x.Slug == request.Slug);

        if (existingCategory is true) return CategoryErrors.Duplicate;

        var category = mapper.Map<Category>(request);
        category.Image = await imageUploader.UploadImageAsync(request.Image, "categories");

        await categoriesRepository.CreateAsync(category);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("دسته بندی با موفقیت ایجاد شد.");
    }
}
