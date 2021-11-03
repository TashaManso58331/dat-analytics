using Dat.Access.Clients;
using Dat.Access.Loads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dat.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchLoadsController : ControllerBase
    {
        private readonly ILogger<SearchLoadsController> log;
        private readonly IDatService datClient;
        private readonly ISearchService searchService;

        public SearchLoadsController(ILogger<SearchLoadsController> logger, IDatService datClient, ISearchService searchService)
        {
            this.log = logger;
            this.datClient = datClient;
            this.searchService = searchService;
        }

        [HttpGet]
        public async Task<List<Load>> Get()
        {
            try
            {
                var userToken = datClient.GetAllTokens();
                var filter = SearchRequest.Create();
                var loads = await searchService.Search(filter);
                return loads;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get user token sessionAccount={0} userAccount={1}");
                throw;
            }
        }
    }
}
