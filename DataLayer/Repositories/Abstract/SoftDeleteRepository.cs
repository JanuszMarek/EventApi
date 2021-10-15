using AutoMapper;
using Entities.Interfaces;
using Infrastructure.Interfaces.IRepositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Abstract
{
    public abstract class SoftDeleteRepository<TEntity, TKey> : 
        BaseRepository<TEntity, TKey>, 
        ISoftDeleteRepository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>, ISoftDeleteEntity
        where TKey : struct
    {
        public SoftDeleteRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public static Expression<Func<TEntity, bool>> IsActive() => x => !x.IsDeleted;

        public async Task<T> GetActiveAsync<T>(TKey id)
        {
            var predicates = new List<Expression<Func<TEntity, bool>>>()
            {
                ByIdExpression(id),
                IsActive()
            };

            return await GetProjectableQuery<T>(predicates).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetActiveListAsync<T>()
        {
            var predicates = new List<Expression<Func<TEntity, bool>>>()
            {
                IsActive()
            };

            return await GetProjectableQuery<T>(predicates).ToListAsync();
        }

        public async Task<TEntity> GetActiveAsync(TKey id)
        {
            return await GetActiveAsync(ByIdExpression(id), null);
        }

        public async Task<TEntity> GetActiveAsync(Expression<Func<TEntity, bool>> wherePredicte, IEnumerable<string> includes)
        {
            var predicates = new List<Expression<Func<TEntity, bool>>>()
            {
                wherePredicte,
                IsActive()
            };

            return await GetWhereIncludeQuery(predicates, includes).FirstOrDefaultAsync();
        }
    }
}
