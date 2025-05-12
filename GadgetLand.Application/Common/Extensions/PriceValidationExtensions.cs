using FluentValidation;
using GadgetLand.Application.Common.Helpers;

namespace GadgetLand.Application.Common.Extensions;

public static class PriceValidationExtensions
{
    public static IRuleBuilderOptions<T, string?> MustBeValidFormattedPrice<T>(this IRuleBuilder<T, string?> ruleBuilder, string errorMessage)
    {
        return ruleBuilder
            .Must(price => string.IsNullOrWhiteSpace(price) || PriceValidationHelper.BeValidFormattedPrice(price))
            .WithMessage(errorMessage);
    }
}
