using Dat.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dat.Access.Caches
{
    public class SimpleMemoryCache<TItem> where TItem : IAccessToken
    {
        private static Object obj = new object();
        private readonly ILogger log;
        private readonly IMemoryCache cache;
        private readonly HashSet<object> availableKeys = new HashSet<object>();
        private readonly string stateFilePath;
        public SimpleMemoryCache(IMemoryCache cache, ILogger log, string prefix)
        {
            this.cache = cache;
            this.log = log;
            this.stateFilePath = Path.Combine(Path.GetTempPath(), prefix);
        }

        /*
         * IMemoryCache is "thread-safe" for multi threaded apps like web sites, 
         * but that does not mean it guarantees to only execute the delegate to prime the cache only once!
         * So let's lock the cache.TryGetValue
         */
        public TItem GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            if (cache.TryGetValue(key, out TItem cacheEntry))
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
                    availableKeys.Add(key);
                }
                return cacheEntry;
            }
        }

        internal async Task SaveState()
        {
            try
            {
                Dictionary<object, TItem> state = new Dictionary<object, TItem>(availableKeys.Count);
                foreach (var key in availableKeys)
                {
                    if (cache.TryGetValue(key, out TItem cacheEntry))
                    {
                        state.Add(key, cacheEntry);
                    }
                }
                var text = Newtonsoft.Json.JsonConvert.SerializeObject(state);
                await File.WriteAllTextAsync(this.stateFilePath, text);
                log.LogDebug("State is saved to {0}", this.stateFilePath);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "LoadState failed stateFilePath={0}", stateFilePath);
            }
        }

        internal void LoadState()
        {
            try
            {
                if (!File.Exists(this.stateFilePath))
                    return;

                var text = File.ReadAllText(this.stateFilePath);
                log.LogDebug("State is loaded from {0}", this.stateFilePath);

                var state = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<object, TItem>>(text);
                foreach (var kvp in state)
                {
                    this.GetOrCreate(kvp.Key, () => Task.Run(() => kvp.Value));
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "LoadState failed stateFilePath={0}", stateFilePath);
            }
        }
    }
}
