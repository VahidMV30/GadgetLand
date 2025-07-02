namespace GadgetLand.Contracts.Payments;

public record VerifyPaymentResponse
{
    public int OrderId { get; set; }
    public string Message { get; set; } = string.Empty;
}
