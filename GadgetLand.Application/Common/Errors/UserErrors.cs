using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(code: "User.NotFound", description: "کاربر یافت نشد.");
}
