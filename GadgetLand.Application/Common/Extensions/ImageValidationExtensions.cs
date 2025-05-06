using FluentValidation;
using GadgetLand.Application.Common.Helpers;
using Microsoft.AspNetCore.Http;

namespace GadgetLand.Application.Common.Extensions;

public static class ImageValidationExtensions
{
    public static IRuleBuilderOptions<T, IFormFile?> MustBeValidImage<T>(this IRuleBuilder<T, IFormFile?> ruleBuilder)
    {
        return ruleBuilder.Must(ImageValidationHelper.BeAValidImageType).WithMessage("فرمت عکس نامعتبر است.");
    }

    public static IRuleBuilderOptions<T, IFormFile?> MustBeUnder1MB<T>(this IRuleBuilder<T, IFormFile?> ruleBuilder)
    {
        return ruleBuilder.Must(ImageValidationHelper.BeUnder1MB).WithMessage("حجم عکس باید کمتر از 1 مگابایت باشد.");
    }
}
