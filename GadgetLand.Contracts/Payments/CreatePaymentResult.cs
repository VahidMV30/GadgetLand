namespace GadgetLand.Contracts.Payments;

public record CreatePaymentResult
{
    public bool IsSuccess { get; set; }
    public string Authority { get; set; } = string.Empty;
    public string PaymentUrl { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
