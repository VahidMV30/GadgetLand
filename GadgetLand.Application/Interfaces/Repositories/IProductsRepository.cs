using GadgetLand.Contracts.Products;
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
    Task<(int totalCount, IEnumerable<Product> products)> GetProductsWithFiltersAsync(
        string? categorySlug, string? brandSlug, bool onlyDiscounted, ProductSortOrder sortOrder, int pageIndex, int pageSize);
    Task<Product?> GetProductDetailsBySlugAsync(string slug);
    Task<IEnumerable<Product>> GetCartProductsByIdsAsync(List<int> ids);
    Task<IEnumerable<Product>> GetDiscountedProductsAsync(int count);
    Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count);
    Task<IEnumerable<Product>> GetLatestProductsAsync(int count);
}
