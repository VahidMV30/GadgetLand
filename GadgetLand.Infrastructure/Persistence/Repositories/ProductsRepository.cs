using System.Data;
using Dapper;
using GadgetLand.Application.Interfaces.Repositories;
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
}
