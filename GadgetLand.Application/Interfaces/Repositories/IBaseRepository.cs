using System.Linq.Expressions;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IBaseRepository<TKey, T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(TKey id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    void Update(T entity);
}
