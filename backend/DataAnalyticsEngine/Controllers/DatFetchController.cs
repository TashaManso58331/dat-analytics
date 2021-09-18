using Dat.Engines;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalyticsEngine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class DatFetchController : ControllerBase
    {
        private readonly ILogger<DatFetchController> _logger;
        private readonly IActionResult preparedJson;

        public DatFetchController(ILogger<DatFetchController> logger)
        {
            _logger = logger;
            preparedJson = PreparedJson();            
        }

        private IActionResult PreparedJson()
        {
            var datOffers = Dat.Test.Utils.GetTestOfferData();
            var datStates = Dat.Test.Utils.GetTestStatesData();
            var engine = new SplitterEngine(new List<ISplitter>
            {
                new PrepareSplitter(),
                new CostOfMileSplitter(),
                new StateSplitter(datStates),
                new TotalCostSplitter(500),
                new TotalWeightSplitter(),
                new DeadHeadOriginSplitter(),
                new PostSetterSplitter()
            });
            var resultData = engine.Run(datOffers);
            var output = resultData.matchDetails.Select(c =>
                new
                {
                    Id = c.matchId,
                    Origin = $"{c.origin.city},{c.origin.state}",
                    Destination = $"{c.destination.city},{c.destination.state}",
                    DHO = c.originDeadheadMiles,
                    Trip = c.tripMiles,
                    Weight = $"{c.weight.ToString("N0")} lbs",
                    Rate = c.rate,
                    Company = c.companyName,
                    Contact = $"{c.contactName.first} {c.contactName.last}",
                    W_CostOfMile = c.CostOfMileSplitter.ToString("N2"),
                    W_State = c.StateSplitter.ToString("N2"),
                    W_Rate = c.TotalCostSplitter.ToString("N2"),
                    W_Summary = c.TotalWeightSplitter.ToString("N2"),
                    W_DHO = c.DeadHeadOriginSplitter.ToString("N2")
                }).ToList();

            return new JsonResult(output, new Newtonsoft.Json.JsonSerializerSettings
            {});
        }

        [HttpGet]
        public IActionResult Get()
        {
            return preparedJson;
        }
    }
}
