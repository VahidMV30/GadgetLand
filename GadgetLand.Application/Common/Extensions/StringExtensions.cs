using System.Text.RegularExpressions;

namespace GadgetLand.Application.Common.Extensions;

public static class StringExtensions
{
    public static string NormalizeSpaces(this string input)
    {
        return string.IsNullOrWhiteSpace(input) ? input : Regex.Replace(input.Trim(), @"\s+", " ");
    }

    public static long? ParsePriceToLong(this string priceWithSeparators)
    {
        if (string.IsNullOrWhiteSpace(priceWithSeparators)) return null;

        var digitOnly = priceWithSeparators.Replace(".", "");
        return long.Parse(digitOnly);
    }

    public static string ParsePriceToString(this long originalPrice)
    {
        return originalPrice.ToString("#,0").Replace(",", ".");
    }

    public static string? ParsePriceToString(this long? originalPrice)
    {
        return originalPrice?.ParsePriceToString();
    }
}
