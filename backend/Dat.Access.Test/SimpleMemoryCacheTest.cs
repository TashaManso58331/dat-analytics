using Dat.Access.Caches;
using Dat.Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dat.Access.Test
{
    [TestFixture]
    class SimpleMemoryCacheTest
    {
        [Test]
        public void ShouldBeCalledOnce()
        {
            var simpleMemoryCache = new Caches.SimpleMemoryCache<Model.AccessToken>(new DatMemoryCache().Cache);

            int counter = 0;

            Parallel.ForEach(Enumerable.Range(1, 10), i =>
            {
                var item = simpleMemoryCache.GetOrCreate("test-key", async () =>
                {
                    Interlocked.Increment(ref counter);
                    return AccessToken.CreateToken("token", DateTime.UtcNow.AddHours(1));
                });
            });
            Assert.AreEqual(1, counter);
        }
    }
}
