using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IProductsRepository : IBaseRepository<int, Product>
{
    Task<Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsForAdminTableAsync();
}
