using Dat.Access.Caches;
using Dat.Access.Clients;
using Dat.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dat.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchLoadsController : ControllerBase
    {
        private readonly ILogger<SearchLoadsController> log;
        private readonly SimpleMemoryCache<AccessToken> userTokenCache;
        private readonly string userAccount;

        public SearchLoadsController(ILogger<SearchLoadsController> logger, IMemoryCache memoryCache, IConfiguration config, IDatService datClient)
        {
            this.log = logger;
            this.userTokenCache = new SimpleMemoryCache<AccessToken>(memoryCache);
            this.userAccount = config[AccessController.cUserAccount] ?? throw new ArgumentNullException(AccessController.cUserAccount);
        }

        [HttpGet]
        public async Task<AccessToken> Get()
        {
            try
            {
                var userToken = userTokenCache.GetOrCreate(userAccount, () => throw new ArgumentException("User token is not in cache"));
                return userToken;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get user token sessionAccount={0} userAccount={1}");
                throw;
            }
        }
    }
}
