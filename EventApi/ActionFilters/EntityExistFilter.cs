using Entities.Interfaces.Abstract;
using EventApi.Constants;
using Infrastructure.Extensions;
using Infrastructure.Interfaces.IRepositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EventApi.ActionFilters
{
    public class EntityExistFilter<TEntity, TKey> : IAsyncActionFilter
            where TEntity : class, IEntity<TKey>
            where TKey : struct
    {
        private readonly IBaseRepository<TEntity, TKey> repository;
        

        public EntityExistFilter(IBaseRepository<TEntity, TKey> repository)
        {
            this.repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            TKey id;
            var entityName = typeof(TEntity).Name;
            var idName = entityName.LowercaseFirst() + "Id";

            if (context.RouteData.Values.Keys.Contains(idName))
            {
                var converted = TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromString(context.RouteData.Values[idName].ToString());
                if (converted == null)
                {
                    context.Result = new BadRequestObjectResult(ResponseMessages.ID_WRONG_FORMAT);
                    return;
                }
                id = (TKey)converted;
            }
            else
            {
                context.Result = new BadRequestObjectResult(ResponseMessages.ID_NOT_FOUND);
                return;
            }

            var enityExists = await repository.ExistAsync(id);
            if (!enityExists)
            {
                string msg = string.Format(ResponseMessages.DATA_NOT_EXIST, entityName, id);
                context.Result = new NotFoundObjectResult(msg);
                return;
            }
            await next();
        }
    }
}
