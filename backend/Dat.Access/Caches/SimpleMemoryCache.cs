using Dat.Model;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Dat.Access.Caches
{
    public class SimpleMemoryCache<TItem> where TItem : IAccessToken
    {
        private IMemoryCache _cache;
        public SimpleMemoryCache(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public TItem GetOrCreate(object key, Func<TItem> createItem)
        {
            TItem cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry)) 
            {
                cacheEntry = createItem();
                var dateTimeOffset = cacheEntry.GetExpiry() - DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Set cache entry size by extension method.
                            .SetSize(1)
                            // Keep in cache for this time, reset time if accessed.
                            .SetAbsoluteExpiration(dateTimeOffset);
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
    }
}
