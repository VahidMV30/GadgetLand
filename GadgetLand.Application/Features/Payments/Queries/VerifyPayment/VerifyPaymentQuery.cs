using ErrorOr;
using GadgetLand.Contracts.Payments;
using MediatR;

namespace GadgetLand.Application.Features.Payments.Queries.VerifyPayment;

public record VerifyPaymentQuery(string Status, string Authority) : IRequest<ErrorOr<VerifyPaymentResponse>>;
