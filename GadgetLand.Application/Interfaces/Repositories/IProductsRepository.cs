using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IProductsRepository : IBaseRepository<int, Product>
{
    Task<IEnumerable<Product>> GetProductsWithDetailsAsync();
}
