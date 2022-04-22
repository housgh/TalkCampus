using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineAdvising.Data;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity: class
    {
        ApplicationDbContext DbContext { get; }
        Task<TEntity> GetAsync(TKey id);
        Task<List<TEntity>> GetAsync();
        Task<int> AddAsync(TEntity entity);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TKey id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAllAsync();
    }
}