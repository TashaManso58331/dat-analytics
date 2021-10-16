using Dat.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Dat.Access.Caches
{
    public class SimpleMemoryCache<TItem> where TItem : IAccessToken
    {
        private static Object obj = new object();
        private IMemoryCache cache;
        public SimpleMemoryCache(IMemoryCache cache)
        {
            this.cache = cache;
        }

        /*
         * IMemoryCache is "thread-safe" for multi threaded apps like web sites, 
         * but that does not mean it guarantees to only execute the delegate to prime the cache only once!
         * So let's lock the cache.TryGetValue
         */
        public TItem GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            TItem cacheEntry;
            if (cache.TryGetValue(key, out cacheEntry))
            {
                return cacheEntry;
            }

            lock (obj)
            {
                if (!cache.TryGetValue(key, out cacheEntry))
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
                    cache.Set(key, cacheEntry, cacheEntryOptions);
                }
                return cacheEntry;
            }
        }
    }
}
