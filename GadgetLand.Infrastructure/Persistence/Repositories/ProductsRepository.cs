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
        return await dbContext.Products
            .Include(product => product.Category)
            .Include(product => product.Brand)
            .AsNoTracking()
            .ToListAsync();
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

        const string query = "dbo.ProductsWithFilters";

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

    public async Task<IEnumerable<Product>> GetCartProductsByIdsAsync(List<int> ids)
    {
        return await dbContext.Products.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetDiscountedProductsAsync(int count)
    {
        return await dbContext.Products
            .Where(product => product.DiscountPrice != null)
            .OrderByDescending(product => Math.Round(((decimal)(product.Price - product.DiscountPrice!.Value) / product.Price) * 100, 0))
            .Take(count)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count)
    {
        var query = dbContext.OrderItems
            .GroupBy(oi => oi.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                MostSales = g.Sum(oi => oi.Quantity)
            })
            .Join(dbContext.Products,
                ms => ms.ProductId,
                p => p.Id,
                (ms, p) => new
                {
                    Product = new Product
                    {
                        Name = p.Name,
                        Slug = p.Slug,
                        Image = p.Image,
                        Price = p.Price,
                        DiscountPrice = p.DiscountPrice
                    },
                    TotalQuantitySold = ms.MostSales
                }
            )
            .OrderByDescending(x => x.TotalQuantitySold)
            .ThenByDescending(x => x.Product.DiscountPrice)
            .Select(x => x.Product)
            .Take(count);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLatestProductsAsync(int count)
    {
        return await dbContext.Products.OrderByDescending(product => product.Id).Take(count).AsNoTracking().ToListAsync();
    }
}
