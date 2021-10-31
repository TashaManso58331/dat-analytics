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
        private readonly IDatService datClient;

        public SearchLoadsController(ILogger<SearchLoadsController> logger, IDatService datClient)
        {
            this.log = logger;
            this.datClient = datClient;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                var userToken = datClient.GetAllTokens();

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
