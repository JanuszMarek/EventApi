using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.IRepositories.Abstract
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task<TEntity> FindAsync(TKey id);
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetAsync(TKey id, IEnumerable<string> includes);
        Task<T> GetAsync<T>(Expression<Func<TEntity, bool>> wherePredicte);
        Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<TKey> ids);
        Task<IEnumerable<TEntity>> GetRangeAsync(Expression<Func<TEntity, bool>> wherePredicte);
        Task<IEnumerable<TEntity>> GetRangeAsync(Expression<Func<TEntity, bool>> wherePredicte, IEnumerable<string> includes);
        Task<IEnumerable<T>> GetRangeAsync<T>(Expression<Func<TEntity, bool>> wherePredicte);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task SaveChangesAsync();
    }
}
