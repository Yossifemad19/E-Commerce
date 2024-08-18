using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface ICachedService
    {
        Task CacheResponseAsync(string cacheKey,object response,TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
