using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Domain.Entities;
using MediatR;

namespace GadgetLand.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler(
    IPaymentService paymentService,
    ISettingsRepository settingsRepository,
    IProductsRepository productsRepository,
    ISecurityService securityService,
    IPaymentsRepository paymentsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreatePaymentCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        if (request.CartItems.Count == 0) return PaymentErrors.Failure("سبد خرید شما خالی است.");

        var setting = await settingsRepository.GetSettingsAsync();
        if (setting is null) return PaymentErrors.Failure("خطایی در بارگذاری تنظیمات رخ داد.");

        var products = (await productsRepository.GetCartProductsByIdsAsync(request.CartItems.Select(x => x.ProductId).ToList())).ToList();
        if (products.Count == 0) return PaymentErrors.Failure("خطایی در بارگذاری محصولات رخ داد.");

        foreach (var cartItem in request.CartItems)
        {
            var product = products.FirstOrDefault(product => product.Id == cartItem.ProductId);

            if (product is null) return PaymentErrors.Failure("خطایی در بارگذاری محصولات رخ داد.");

            if (product.QuantityInStock < cartItem.Quantity) return PaymentErrors.Failure($"موجودی محصول «{product.Name}» کافی نیست. لطفا از سبد خرید اصلاح نمایید.");
        }

        var paymentItems = (from cartItem in request.CartItems
                            let product = products.FirstOrDefault(product => product.Id == cartItem.ProductId)!
                            let totalDiscountAmount = product.DiscountPrice.HasValue ? (product.Price - product.DiscountPrice.Value) * cartItem.Quantity : 0
                            let paymentItemSubtotalAmount = product.Price * cartItem.Quantity
                            select new PaymentItem
                            {
                                ProductId = product.Id,
                                Quantity = cartItem.Quantity,
                                UnitPrice = product.Price,
                                UnitDiscount = product.DiscountPrice ?? 0,
                                TotalDiscountAmount = totalDiscountAmount,
                                SubtotalAmount = paymentItemSubtotalAmount,
                                TotalAmount = paymentItemSubtotalAmount - totalDiscountAmount
                            }).ToList();

        var discountAmount = paymentItems.Sum(x => x.TotalDiscountAmount);
        var shippingCost = setting.ShippingCost;
        var subtotalAmount = paymentItems.Sum(x => x.SubtotalAmount) + shippingCost;
        var totalPayableAmount = subtotalAmount - discountAmount;

        var result = await paymentService.CreatePaymentAsync(totalPayableAmount * 10);
        if (result.IsSuccess is false)
        {
            return PaymentErrors.Failure(result.Message);
        }

        var payment = new Payment
        {
            UserId = Convert.ToInt32(securityService.GetUserIdFromToken()),
            DiscountAmount = discountAmount,
            ShippingCost = shippingCost,
            SubtotalAmount = subtotalAmount,
            TotalPayableAmount = totalPayableAmount,
            Authority = result.Authority,
            IsPaid = false,
            PaymentItems = paymentItems
        };

        await paymentsRepository.CreateAsync(payment);

        await unitOfWork.CommitChangesAsync();

        return result.PaymentUrl;
    }
}
