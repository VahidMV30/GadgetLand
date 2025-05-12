using System.Text.RegularExpressions;
using GadgetLand.Application.Common.Constants;

namespace GadgetLand.Application.Common.Helpers;

public static class PriceValidationHelper
{
    public static bool BeValidFormattedPrice(string? price)
    {
        if (string.IsNullOrWhiteSpace(price)) return false;

        if (Regex.IsMatch(price, RegularExpressions.Price) is false) return false;

        var cleaned = price.Replace(".", "");
        return decimal.TryParse(cleaned, out var value) && value > 0;
    }
}
