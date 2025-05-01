using System.Text.RegularExpressions;

namespace GadgetLand.Application.Common.Extensions;

public static class StringExtensions
{
    public static string NormalizeSpaces(this string input)
    {
        return string.IsNullOrWhiteSpace(input) ? input : Regex.Replace(input.Trim(), @"\s+", " ");
    }
}
