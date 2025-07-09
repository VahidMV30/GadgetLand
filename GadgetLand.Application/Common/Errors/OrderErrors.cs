using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class OrderErrors
{
    public static Error NotFound => Error.NotFound(code: "Order.NotFound", description: "سفارش یافت نشد.");
}
