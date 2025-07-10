using FluentValidation;

namespace GadgetLand.Application.Features.Users.Commands.UpdateUserInfo;

public class UpdateUserInfoCommandValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoCommandValidator()
    {
        RuleFor(x => x.CityId)
            .NotNull().WithMessage("لطفا شهر محل سکونت را مشخص نمایید.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("لطفا نام و نام خانوادگی را وارد نمایید.")
            .Length(3, 50).WithMessage("نام و نام خانوادگی باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("لطفا موبایل را وارد نمایید.")
            .Matches(@"^0\d{10}$").WithMessage("موبایل وارد شده نامعتبر است.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("لطفا کد پستی را وارد نمایید.")
            .Matches(@"^\d{10}$").WithMessage("کد پستی وارد شده نامعتبر است.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("لطفا آدرس را وارد نمایید.")
            .Length(10, 256).WithMessage("آدرس باید حداقل 10 و حداکثر 256 کاراکتر باشد.");
    }
}
