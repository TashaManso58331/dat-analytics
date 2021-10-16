using Dat.Access.Caches;
using Dat.Access.Clients;
using Dat.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Dat.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly ILogger<AccessController> log;
        private readonly SimpleMemoryCache<AccessToken> serviceTokenCache;
        private readonly SimpleMemoryCache<AccessToken> userTokenCache;
        private readonly IDatService datClient;
        private readonly string sessionAccount;
        private readonly string sessionPassword;
        private readonly string userAccount;

        public const string cSessionAccount = "Dat:SessionAccount";
        public const string cSessionPassword = "Dat:SessionPassword";
        public const string cUserAccount = "Dat:UserAccount";

        public AccessController(ILogger<AccessController> logger, IMemoryCache memoryCache, IConfiguration config, IDatService datClient)
        {
            this.log = logger;
            this.serviceTokenCache = new SimpleMemoryCache<AccessToken>(memoryCache);
            this.userTokenCache = new SimpleMemoryCache<AccessToken>(memoryCache);
            this.datClient = datClient;
            this.sessionAccount = config[cSessionAccount] ?? throw new ArgumentNullException(cSessionAccount);
            this.sessionPassword = config[cSessionPassword] ?? throw new ArgumentNullException(cSessionPassword); 
            this.userAccount = config[cUserAccount] ?? throw new ArgumentNullException(cUserAccount);
        }

        [HttpGet]
        public String Get()
        {
            try
            {
                var sessionToken = serviceTokenCache.GetOrCreate(sessionAccount, () => datClient.GetSessionToken(sessionAccount, sessionPassword));
                var userToken = userTokenCache.GetOrCreate(userAccount, () => datClient.GetUserToken(sessionToken, userAccount));
                return userToken?.Token;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get user token sessionAccount={0} userAccount={1}");
                throw;
            }
        }
    }
}
