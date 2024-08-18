using E_Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.Api.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter

    {
        private readonly int _timeTolive;
        

        public CachedAttribute(int timeTolive)
        {
            _timeTolive = timeTolive;
    
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<ICachedService>();
            var cacheKey = generateCacheKey(context.HttpContext.Request);
            
            var cachedResponse =await cachedService.GetCachedResponseAsync(cacheKey);
            
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var response= new OkObjectResult(cachedResponse);

                context.Result= response;
                return;
            }

            var excutedContext = await next();

            if (excutedContext.Result is OkObjectResult okObjectResult ) {
                await cachedService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeTolive));
            }
        }

        private string generateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach ( var (key,value) in request.Query.OrderBy(x=>x.Key) ) {
                keyBuilder.Append($"|{key}---{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
