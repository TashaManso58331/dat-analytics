using Microsoft.Extensions.Caching.Memory;

namespace Dat.Access.Caches
{
    public class DatMemoryCache
    {
        public MemoryCache Cache { get; private set; }
        public DatMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024
            });
        }
    }
}
