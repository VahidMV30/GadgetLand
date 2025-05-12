using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class ProductErrors
{
    public static Error Duplicate => Error.Conflict(code: "Product.Duplicate", description: "محصول قبلا ایجاد شده است.");
    public static Error NotFound => Error.NotFound(code: "Product.NotFound", description: "محصول یافت نشد.");
}
