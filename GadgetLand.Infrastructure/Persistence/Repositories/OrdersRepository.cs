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

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await dbContext.Orders
            .Where(order => order.User.Id == userId)
            .OrderBy(order => order.OrderStatus)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithItemsByIdAsync(int orderId, int userId)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Include(order => order.User)
            .ThenInclude(user => user.City)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            .ThenInclude(city => city.Province)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.Product)
            .FirstOrDefaultAsync(order => order.Id == orderId && order.UserId == userId);
    }

    public async Task<Order?> GetLastOrderWithItemsByUserIdAsync(int userId)
    {
        return await dbContext.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(order => order.Product)
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.UserId)
            .Take(1)
            .FirstOrDefaultAsync();
    }
}
