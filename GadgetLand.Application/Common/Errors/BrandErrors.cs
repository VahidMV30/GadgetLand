using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class BrandErrors
{
    public static Error Duplicate => Error.Conflict(code: "Brand.Duplicate", description: "برند قبلا ایجاد شده است.");
    public static Error NotFound => Error.NotFound(code: "Brand.NotFound", description: "برند یافت نشد.");
}
