using FluentValidation;

namespace GadgetLand.Application.Features.Reviews.Commands;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(0).WithMessage("لطفا محصول مورد نظر را مشخص نمایید.");

        RuleFor(x => x.Rating)
            .NotEqual(0).WithMessage("لطفا امتیاز را مشخص نمایید.");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("لطفا متن دیدگاه را وارد نمایید.")
            .Length(10, 256).WithMessage("متن دیدگاه باید حداقل 10 و حداکثر 256 کاراکتر باشد.");
    }
}
