using ErrorOr;

namespace GadgetLand.Application.Common.Errors;

public static class ReviewErrors
{
    public static Error Duplicate => Error.Conflict(code: "Review.Duplicate", description: "شما قبلا برای محصول مورد نظر دیدگاه ارسال کرده اید.");
}
