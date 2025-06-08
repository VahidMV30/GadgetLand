using System.Data;
using Dapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Products;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class ProductsRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Product>(dbContext), IProductsRepository
{
    private readonly IDbConnection _dbConnection = dbContext.Database.GetDbConnection();

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsForAdminTableAsync()
    {
        const string query = "EXEC GetProductsForAdminTable";

        var productDictionary = new Dictionary<int, Product>();

        await _dbConnection.QueryAsync<Product, Category, Brand, Product>(
            query,
            (product, category, brand) =>
            {
                if (productDictionary.TryGetValue(product.Id, out var existingProduct)) return existingProduct;

                existingProduct = product;
                existingProduct.Category = category;
                existingProduct.Brand = brand;
                productDictionary.Add(existingProduct.Id, existingProduct);

                return existingProduct;
            },
            splitOn: "Id,Id,Id"
        );

        return productDictionary.Values;
    }

    public async Task<Product?> GetProductWithImagesByIdAsync(int id)
    {
        return await dbContext.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ProductImage>> GetProductImagesByFileNamesAsync(string[] imageNames)
    {
        return await dbContext.ProductImages.Where(x => imageNames.Contains(x.Image)).ToListAsync();
    }

    public void RemoveProductImages(IEnumerable<ProductImage> productImages)
    {
        dbContext.ProductImages.RemoveRange(productImages);
    }

    public async Task CreateProductImagesAsync(IEnumerable<ProductImage> entities)
    {
        await dbContext.ProductImages.AddRangeAsync(entities);
    }

    public async Task<(int totalCount, IEnumerable<Product> products)> GetProductsWithFiltersAsync(
        string? categorySlug, string? brandSlug, bool onlyDiscounted, ProductSortOrder sortOrder, int pageIndex, int pageSize)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CategorySlug", categorySlug);
        parameters.Add("@BrandSlug", brandSlug);
        parameters.Add("@OnlyDiscounted", onlyDiscounted);
        parameters.Add("@SortOrder", sortOrder.ToString());
        parameters.Add("@PageIndex", pageIndex);
        parameters.Add("@PageSize", pageSize);

        const string query = "GetProductsWithFilters";

        await using var multi = await _dbConnection.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure);

        var totalCount = await multi.ReadFirstAsync<int>();
        var products = (await multi.ReadAsync<Product>()).ToList();

        return (totalCount, products);
    }

    public async Task<Product?> GetProductDetailsBySlugAsync(string slug)
    {
        return await dbContext.Products
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Include(x => x.ProductImages)
            .Include(x => x.Reviews.Where(y => y.IsConfirmed && y.IsDeleted == false).OrderByDescending(y => y.CreatedAt))
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Slug == slug);
    }
}
