using ErrorOr;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Commands.ChangeOrderStatusById;

public record ChangeOrderStatusByIdCommand(int OrderId, int OrderStatus) : IRequest<ErrorOr<OperationResponse>>;
