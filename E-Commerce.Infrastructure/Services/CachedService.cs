using E_Commerce.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Services
{
    public class CachedService : ICachedService
    {
        private readonly IDatabase _redis;

        public CachedService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
                return ;

            var options=  new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var responseJson=JsonSerializer.Serialize(response, options);

            await _redis.StringSetAsync(cacheKey, responseJson,timeToLive);
        }


        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var response =await _redis.StringGetAsync(cacheKey);

            if (response.IsNullOrEmpty)
                return null;

            return response;
        }
    }
}
