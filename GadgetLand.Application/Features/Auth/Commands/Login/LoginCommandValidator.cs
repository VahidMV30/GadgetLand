using FluentValidation;
using GadgetLand.Application.Common.Constants;

namespace GadgetLand.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("لطفا ایمیل را وارد نمایید.")
            .Matches(RegularExpressions.Email).WithMessage("ایمیل وارد شده نامعتبر است.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("لطفا گذرواژه را وارد نمایید.");
    }
}
