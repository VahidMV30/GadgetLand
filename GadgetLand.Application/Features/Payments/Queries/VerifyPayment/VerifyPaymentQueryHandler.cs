using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Payments;
using GadgetLand.Domain.Entities;
using MediatR;

namespace GadgetLand.Application.Features.Payments.Queries.VerifyPayment;

public class VerifyPaymentQueryHandler(
    IPaymentsRepository paymentsRepository,
    IPaymentService paymentService,
    IProductsRepository productsRepository,
    IMapper mapper,
    IOrdersRepository ordersRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<VerifyPaymentQuery, ErrorOr<VerifyPaymentResponse>>
{
    public async Task<ErrorOr<VerifyPaymentResponse>> Handle(VerifyPaymentQuery request, CancellationToken cancellationToken)
    {
        if (request.Status != "OK")
        {
            return PaymentErrors.Failure("پرداخت با شکست مواجه شد. در صورت کسر وجه از حساب شما، مبلغ مربوطه حداکثر ظرف ۷۲ ساعت کاری به حساب شما بازخواهد گشت.");
        }

        var payment = await paymentsRepository.GetPaymentByAuthorityAsync(request.Authority);
        if (payment is null) return PaymentErrors.Failure("تراکنش مورد نظر یافت نشد.");
        if (payment.IsPaid) return PaymentErrors.Failure("این تراکنش قبلاً با موفقیت انجام شده است.");

        var result = await paymentService.VerifyPaymentAsync(payment.TotalPayableAmount * 10, payment.Authority);
        if (result.IsSuccess is false) return PaymentErrors.Failure(result.Message);

        payment.IsPaid = true;
        payment.PaidAt = DateTime.UtcNow;
        payment.RefId = result.RefId;

        paymentsRepository.Update(payment);

        foreach (var paymentItem in payment.PaymentItems)
        {
            var product = await productsRepository.GetByIdAsync(paymentItem.ProductId);

            product!.QuantityInStock -= paymentItem.Quantity;

            productsRepository.Update(product);
        }

        var order = mapper.Map<Order>(payment);

        await ordersRepository.CreateAsync(order);

        await unitOfWork.CommitChangesAsync();

        return new VerifyPaymentResponse
        {
            OrderId = order.Id,
            Message = $"سفارش شما با کد {order.Id} با موفقیت ثبت شد. کد رهگیری پرداخت: {payment.RefId}"
        };
    }
}
