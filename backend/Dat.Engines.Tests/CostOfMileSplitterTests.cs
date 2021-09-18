using Dat.Model.Offer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dat.Engines.Tests
{
    [TestClass]
    public class CostOfMileSplitterTests
    {
        [TestMethod]
        public void CalcWeight()
        {
            var stateSplitter = new CostOfMileSplitter();
            Assert.AreEqual(1000m, stateSplitter.CalcWeight(new MatchDetail { tripMiles = 0, rate = 1000 })); ;
            Assert.AreEqual(2m, stateSplitter.CalcWeight(new MatchDetail { tripMiles = 1000, rate = 2000 })); ;
        }
    }
}
