using Microsoft.Extensions.Caching.Memory;
using System;

namespace Dat.Access.Caches
{
    public class SimpleMemoryCache<TItem>
    {
        private IMemoryCache _cache;
        public SimpleMemoryCache(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public TItem GetOrCreate(object key, Func<TItem> createItem)
        {
            TItem cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry)) // Ищем ключ в кэше.
            {
                // Ключ отсутствует в кэше, поэтому получаем данные.
                cacheEntry = createItem();

                // Сохраняем данные в кэше. 
                _cache.Set(key, cacheEntry);
            }
            return cacheEntry;
        }
    }
}
