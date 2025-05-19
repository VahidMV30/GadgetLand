using FluentValidation;
using GadgetLand.Application.Common.Extensions;

namespace GadgetLand.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("لطفا دسته بندی محصول را مشخص نمایید.");

        RuleFor(x => x.BrandId)
            .GreaterThan(0).WithMessage("لطفا برند محصول را مشخص نمایید.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("لطفا نام محصول را وارد نمایید.")
            .Length(3, 50).WithMessage("نام محصول باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("لطفا اسلاگ را وارد نمایید.")
            .Length(3, 100).WithMessage("اسلاگ باید حداقل 3 و حداکثر 100 کاراکتر باشد.");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("انتخاب عکس محصول الزامی است.")
            .MustBeValidImage()
            .MustBeUnder1MB();

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("لطفا قیمت محصول را وارد نمایید.")
            .MustBeValidFormattedPrice("قیمت محصول وارد شده نامعتبر است.");

        RuleFor(x => x.DiscountPrice)
            .MustBeValidFormattedPrice("قیمت تخفیف وارد شده نامعتبر است.");

        RuleFor(x => x.QuantityInStock)
            .NotNull().WithMessage("لطفا موجودی انبار را وارد نمایید.")
            .GreaterThanOrEqualTo(0).WithMessage("موجودی انبار نباید عدد منفی باشد.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("لطفا توضیحات را وارد نمایید.")
            .Length(20, 1024).WithMessage("توضیحات باید حداقل 20 و حداکثر 1024 کاراکتر باشد.");

        RuleFor(x => x)
            .Must(x =>
            {
                if (string.IsNullOrWhiteSpace(x.DiscountPrice)) return true;

                var price = x.Price.ParsePriceToLong();
                var discountPrice = x.DiscountPrice.ParsePriceToLong();

                return discountPrice < price;
            }).WithMessage("قیمت تخفیف نباید بزرگتر یا مساوی قیمت اصلی باشد.");
    }
}
