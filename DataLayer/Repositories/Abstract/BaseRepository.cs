using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities.Interfaces.Abstract;
using Infrastructure.Interfaces.IRepositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Abstract
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

        public static Expression<Func<TEntity, bool>> ByIdExpression(TKey id) => x => x.Id.Equals(id);

        public static Expression<Func<TEntity, bool>> ByIdRangeExpression(IEnumerable<TKey> id) => x => id.Contains(x.Id);


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

        public async Task<bool> ExistAsync(TKey id)
        {
            return await GetWhereIncludeQuery(ByIdExpression(id), null).AnyAsync();
        }

        public async Task<TEntity> FindAsync(TKey id)
        {
            return await dbSet
                .FindAsync(id);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await GetAsync(id, null);
        }

        public async Task<TEntity> GetAsync(TKey id, IEnumerable<string> includes)
        {
            var query = GetWhereIncludeQuery(ByIdExpression(id), includes);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync<T>(TKey id)
        {
            return await GetAsync<T>(ByIdExpression(id));
        }

        public async Task<T> GetAsync<T>(Expression<Func<TEntity, bool>> wherePredicte)
        {
            return await GetProjectableQuery<T>(wherePredicte)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<TKey> ids)
        {
            return await GetRangeAsync(ByIdRangeExpression(ids));
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(Expression<Func<TEntity, bool>> wherePredicte)
        {
            return await GetRangeAsync(wherePredicte, null);
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(Expression<Func<TEntity, bool>> wherePredicte, IEnumerable<string> includes)
        {
            IQueryable<TEntity> query = GetWhereIncludeQuery(wherePredicte, includes);

            return await query.ToListAsync();
        }

        protected IQueryable<TEntity> GetWhereIncludeQuery(Expression<Func<TEntity, bool>> wherePredictes, IEnumerable<string> includes)
        {
            var query = dbSet
                .AsQueryable();

            if (wherePredictes != null)
            {
                query = query.Where(wherePredictes);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public async Task<IEnumerable<T>> GetRangeAsync<T>()
        {
            return await GetRangeAsync<T>(null);
        }

        public async Task<IEnumerable<T>> GetRangeAsync<T>(Expression<Func<TEntity, bool>> wherePredicte)
        {
            return await GetProjectableQuery<T>(wherePredicte)
                .ToListAsync();
        }

        protected IQueryable<T> GetProjectableQuery<T>(Expression<Func<TEntity, bool>> wherePredictes)
        {
            var query = dbSet.AsQueryable();

            if (wherePredictes != null)
            {
                query = query.Where(wherePredictes);
            }

            return query
                .AsNoTracking()
                .ProjectTo<T>(mapper.ConfigurationProvider);
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
