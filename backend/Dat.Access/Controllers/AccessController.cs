using Dat.Access.Caches;
using Dat.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dat.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly ILogger<AccessController> _logger;
        private readonly SimpleMemoryCache<AccessToken> serviceTokenCache;
        private readonly SimpleMemoryCache<AccessToken> userTokenCache;

        public AccessController(ILogger<AccessController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            serviceTokenCache = new SimpleMemoryCache<AccessToken>(memoryCache);
            userTokenCache = new SimpleMemoryCache<AccessToken>(memoryCache);
        }

        [HttpGet]
        public AccessToken Get()
        {
            if (_userAccessTokenCache.TryGetValue<AccessToken>())
            
            var accessToken = GetAccessTokenFromCache();
            if (accessToken != null)
                return accessToken;

            var sessionToken = GetTokenForOrganization();
            accessToken = GetTokenForUserAccount(sessionToken);
            PutToCache(accessToken);
            return accessToken;
        }

        public static class CacheKeys
        {
            public static string UserName => "_Entry";
        }
    }
}
