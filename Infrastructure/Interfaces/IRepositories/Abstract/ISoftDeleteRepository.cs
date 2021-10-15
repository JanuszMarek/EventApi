using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.IRepositories.Abstract
{
    public interface ISoftDeleteRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, ISoftDeleteEntity
        where TKey : struct
    {
        Task<T> GetActiveAsync<T>(TKey id);
        Task<TEntity> GetActiveAsync(Expression<Func<TEntity, bool>> wherePredicte, IEnumerable<string> includes);
        Task<TEntity> GetActiveAsync(TKey id);
    }
}