using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class PaymentErrors
{
    public static Error Failure(string message) => Error.Failure(code: "Payment.Failure", description: message);
}
