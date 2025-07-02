namespace GadgetLand.Contracts.Payments;

public record VerifyPaymentResult
{
    public bool IsSuccess { get; set; }
    public string Authority { get; set; } = string.Empty;
    public long RefId { get; set; }
    public string Message { get; set; } = string.Empty;
}
