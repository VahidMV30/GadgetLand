using FluentValidation;
using GadgetLand.Application.Common.Constants;

namespace GadgetLand.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("لطفا نام و نام خانوادگی را وارد نمایید.")
            .Length(3, 50).WithMessage("نام و نام خانوادگی باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("لطفا ایمیل را وارد نمایید.")
            .Matches(RegularExpressions.Email).WithMessage("ایمیل وارد شده نامعتبر است.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("لطفا گذرواژه را وارد نمایید.")
            .MinimumLength(6).WithMessage("گذرواژه باید حداقل 6 کاراکتر باشد");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("لطفا گذرواژه را تائید نمایید.")
            .Equal(x => x.Password).WithMessage("گذرواژه و تائید آن مطابقت ندارد.");
    }
}
