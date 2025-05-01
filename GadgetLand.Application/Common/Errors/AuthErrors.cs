using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class AuthErrors
{
    public static Error Duplicate => Error.Conflict(code: "Auth.Duplicate", description: "شما قبلا ثبت نام کرده اید.");
    public static Error InvalidCredentials => Error.Failure(code: "Auth.InvalidCredentials", description: "ایمیل یا گذرواژه صحیح نیست.");
}
