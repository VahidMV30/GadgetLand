using System.Data;
using Dapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class ProductsRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Product>(dbContext), IProductsRepository
{
    private readonly IDbConnection _dbConnection = dbContext.Database.GetDbConnection();

    public async Task<IEnumerable<Product>> GetProductsWithDetailsAsync()
    {
        const string query = "EXEC GetProductsWithDetails";

        var productDictionary = new Dictionary<int, Product>();

        await _dbConnection.QueryAsync<Product, Category, Brand, ProductImage, Product>(
            query,
            (product, category, brand, productImage) =>
            {
                if (!productDictionary.TryGetValue(product.Id, out var existingProduct))
                {
                    existingProduct = product;
                    existingProduct.Category = category;
                    existingProduct.Brand = brand;
                    existingProduct.ProductImages = new List<ProductImage>();
                    productDictionary.Add(existingProduct.Id, existingProduct);
                }

                existingProduct.ProductImages.Add(productImage);

                return existingProduct;
            },
            splitOn: "Id,Id,Id"
        );


        return productDictionary.Values;
    }
}
