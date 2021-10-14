using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities.Interfaces;
using Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        protected readonly IMapper mapper;
        protected readonly DbSet<TEntity> dbSet;
        protected readonly EventContext context;

        protected BaseRepository(EventContext context, IMapper mapper)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
            this.mapper = mapper;
        }

        public static Expression<Func<TEntity, bool>> ByIdExpression(TKey id)
        {
            return x => x.Id.Equals(id);
        }

        public static Expression<Func<TEntity, bool>> ByIdRangeExpression(IEnumerable<TKey> id)
        {
            return x => id.Contains(x.Id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> FindAsync(TKey id)
        {
            return await dbSet
                .FindAsync(id);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await dbSet
                .Where(ByIdExpression(id))
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync<T>(Expression<Func<TEntity, bool>> wherePredicte)
        {
            return await dbSet
                .Where(wherePredicte)
                .AsNoTracking()
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<TKey> ids)
        {
            return await dbSet
                .Where(ByIdRangeExpression(ids))
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(Expression<Func<TEntity, bool>> wherePredicte)
        {
            return await dbSet
                .Where(wherePredicte)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetRangeAsync<T>(Expression<Func<TEntity, bool>> wherePredicte)
        {
            return await dbSet
                .Where(wherePredicte)
                .AsNoTracking()
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
