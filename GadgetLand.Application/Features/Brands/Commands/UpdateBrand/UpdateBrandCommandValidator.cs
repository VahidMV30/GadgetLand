using FluentValidation;
using GadgetLand.Application.Common.Extensions;

namespace GadgetLand.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("لطفا نام برند را وارد نمایید.")
            .Length(3, 50).WithMessage("نام برند باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("لطفا اسلاگ را وارد نمایید.")
            .Length(3, 100).WithMessage("اسلاگ باید حداقل 3 و حداکثر 100 کاراکتر باشد.");

        RuleFor(x => x.Image)
            .MustBeValidImage()
            .MustBeUnder1MB();
    }
}
