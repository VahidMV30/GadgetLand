using GadgetLand.Contracts.Payments;

namespace GadgetLand.Application.Interfaces.Services;

public interface IPaymentService
{
    Task<CreatePaymentResult> CreatePaymentAsync(long amount);
    Task<VerifyPaymentResult> VerifyPaymentAsync(long amount, string authority);
}
