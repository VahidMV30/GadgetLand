using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class OrdersRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Order>(dbContext), IOrdersRepository
{
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await dbContext.Orders
            .Include(order => order.User)
            .OrderBy(order => order.OrderStatus)
            .ThenByDescending(order => order.OrderDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithItemsAndUserByIdAsync(int orderId)
    {
        return await dbContext.Orders
            .Include(order => order.User)
            .ThenInclude(user => user.City)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            .ThenInclude(city => city.Province)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == orderId);
    }
}
