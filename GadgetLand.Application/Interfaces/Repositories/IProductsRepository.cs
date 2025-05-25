using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IProductsRepository : IBaseRepository<int, Product>
{
    Task<Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsForAdminTableAsync();
    Task<Product?> GetProductWithImagesByIdAsync(int id);
    Task<IEnumerable<ProductImage>> GetProductImagesByFileNamesAsync(string[] imageNames);
    void RemoveProductImages(IEnumerable<ProductImage> productImages);
    Task CreateProductImagesAsync(IEnumerable<ProductImage> entities);
}
