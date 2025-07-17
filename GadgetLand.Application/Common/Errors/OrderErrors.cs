using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class OrderErrors
{
    public static Error NotFound => Error.NotFound(code: "Order.NotFound", description: "سفارش یافت نشد.");
    public static Error LastOrderNotFound => Error.NotFound(code: "Order.LastOrderNotFound", description: "شما هیچ سفارشی ثبت نکرده اید.");
}
