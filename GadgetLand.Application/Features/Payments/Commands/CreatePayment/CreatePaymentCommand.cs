using ErrorOr;
using GadgetLand.Contracts.Payments;
using MediatR;

namespace GadgetLand.Application.Features.Payments.Commands.CreatePayment;

public record CreatePaymentCommand(List<CartItem> CartItems) : IRequest<ErrorOr<string>>;
