using FluentValidation;
using GadgetLand.Application.Common.Extensions;

namespace GadgetLand.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("لطفا نام دسته بندی را وارد نمایید.")
            .Length(3, 50).WithMessage("نام دسته بندی باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("لطفا اسلاگ را وارد نمایید.")
            .Length(3, 100).WithMessage("اسلاگ باید حداقل 3 و حداکثر 100 کاراکتر باشد.");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("انتخاب عکس دسته بندی الزامی است.")
            .MustBeValidImage()
            .MustBeUnder1MB();
    }
}
