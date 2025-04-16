using System.Linq.Expressions;
using GadgetLand.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class RepositoryBase<TKey, T>(DbContext dbContext) : IRepositoryBase<TKey, T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(TKey id)
    {
        return await dbContext.FindAsync<T>(id);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
    {
        return await dbContext.Set<T>().AnyAsync(expression);
    }

    public async Task CreateAsync(T entity)
    {
        await dbContext.AddAsync(entity);
    }

    public void Update(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }
}
