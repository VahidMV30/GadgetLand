using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class SettingErrors
{
    public static Error NotFound => Error.NotFound(code: "Settings.NotFound", description: "تنظیمات یافت نشد.");
}
