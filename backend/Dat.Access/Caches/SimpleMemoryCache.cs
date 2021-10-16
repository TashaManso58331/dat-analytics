using Dat.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Dat.Access.Caches
{
    public class SimpleMemoryCache<TItem> where TItem : IAccessToken
    {
        private IMemoryCache _cache;
        public SimpleMemoryCache(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public TItem GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            TItem cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = Task.Run(async () => await createItem())
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Set cache entry size by extension method.
                            .SetSize(1)
                            // Keep in cache for this time, reset time if accessed.
                            .SetAbsoluteExpiration(cacheEntry.GetExpiry());
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
    }
}
