using Entities.Interfaces.Abstract;
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
            //var repository = (IBaseRepository<TEntity, TKey>)context.HttpContext.RequestServices.GetService(typeof(IBaseRepository<TEntity, TKey>));

            if (context.RouteData.Values.Keys.Contains(idName))
            {
                var converted = TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromString(context.RouteData.Values[idName].ToString());
                if (converted == null)
                {
                    string msg = "Wrong Id format";
                    context.Result = new BadRequestObjectResult(msg);
                    return;
                }
                id = (TKey)converted;
            }
            else
            {
                string msg = "Id could not be found in parameters";
                context.Result = new BadRequestObjectResult(msg);
                return;
            }

            var enityExists = await repository.ExistAsync(id);
            if (!enityExists)
            {
                string msg = $"Data with id {id} does not exist or is deleted.";
                context.Result = new NotFoundObjectResult(msg);
                return;
            }
            await next();
        }
    }
}
