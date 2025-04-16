using System.Text.RegularExpressions;

namespace GadgetLand.Application.Common.Extensions;

public static class StringExtensions
{
    public static string NormalizeSpaces(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        return Regex.Replace(input.Trim(), @"\s+", " ");
    }
}
