using System.Linq.Expressions;

namespace Common.Interfaces;
public interface IBaseRepository<T>
{
    Task<T> AddSingleAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  int? top = null,
                                  int? skip = null,
                                  params string[] includeProperties);
}