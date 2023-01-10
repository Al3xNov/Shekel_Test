using System.Linq.Expressions;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    #region Readonlys

    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    #endregion

    #region Constructor
    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    #endregion

    #region Methods

    public async Task<T> AddSingleAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// Gets a collection of entities based on the specified criteria.
    /// </summary>
    /// <param name="filter">The condition the entities must fulfil to be returned</param>
    /// <param name="orderBy">The function used to order the entities</param>
    /// <param name="top">The number of records to limit the results to</param>
    /// <param name="skip">The number of records to skip</param>
    /// <param name="includeProperties">Any other navigation properties to include when returning the collection</param>
    /// <returns>A collection of entities</returns>
    public async Task<IEnumerable<T>> GetManyAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? top = null,
        int? skip = null,
        params string[] includeProperties)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties.Length > 0)
        {
            query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (top.HasValue)
        {
            query = query.Take(top.Value);
        }

        return await query.ToListAsync();
    }

    #endregion
}