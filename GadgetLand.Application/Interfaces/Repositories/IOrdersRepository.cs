using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IOrdersRepository : IBaseRepository<int, Order>
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderWithItemsAndUserByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    Task<Order?> GetOrderWithItemsByIdAsync(int orderId, int userId);
}
