using Dat.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dat.Engines.Tests
{
    [TestClass]
    public class AllSplittersTests
    {
        [TestMethod]
        public void Load()
        {
            var sourceData = Utils.GetTestOfferData();
            var states = Utils.GetTestStatesData();

            var engine = new SplitterEngine(new List<ISplitter>
            {
                new Dat.Engines.PrepareSplitter(),
                new CostOfMileSplitter(),
                new StateSplitter(states),
                new TotalCostSplitter(500),
                new TotalWeightSplitter(),
                new DeadHeadOriginSplitter(),
                new PostSetterSplitter()
            });
            var resultData = engine.Run(sourceData);
            string json = JsonConvert.SerializeObject(resultData);
            Assert.IsTrue(json.Length > 1000);
        }
    }
}
