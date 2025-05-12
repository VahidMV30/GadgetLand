namespace GadgetLand.Application.Common.Constants;

public class RegularExpressions
{
    public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public const string Price = @"^\d{1,3}(?:\.\d{3})*$";
}
