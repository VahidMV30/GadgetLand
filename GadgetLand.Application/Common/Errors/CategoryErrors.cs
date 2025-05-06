using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class CategoryErrors
{
    public static Error Duplicate => Error.Conflict(code: "Category.Duplicate", description: "دسته بندی قبلا ایجاد شده است.");
    public static Error NotFound => Error.NotFound(code: "Category.NotFound", description: "دسته بندی یافت نشد.");
}
