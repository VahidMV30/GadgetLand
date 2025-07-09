using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts;
using GadgetLand.Domain.Enums;
using MediatR;

namespace GadgetLand.Application.Features.Orders.Commands.ChangeOrderStatusById;

public class ChangeOrderStatusByIdCommandHandler(
    IOrdersRepository ordersRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ChangeOrderStatusByIdCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(ChangeOrderStatusByIdCommand request, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetByIdAsync(request.OrderId);

        if (order is null) return OrderErrors.NotFound;

        order.OrderStatus = (OrderStatus)request.OrderStatus;

        ordersRepository.Update(order);

        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("وضعیت سفارش با موفقیت تغییر کرد.");
    }
}
