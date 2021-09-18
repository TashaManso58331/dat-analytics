using Dat.Model.Offer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dat.Engines.Tests
{
    [TestClass]
    public class DeadHeadOriginSplitterTests
    {
        [TestMethod]
        public void CalcWeight()
        {
            var stateSplitter = new DeadHeadOriginSplitter();
            Assert.AreEqual(0m, stateSplitter.CalcWeight(new MatchDetail { originDeadheadMiles = 10000 }));
            Assert.AreEqual(0m, stateSplitter.CalcWeight(new MatchDetail { originDeadheadMiles = 100 }));
            Assert.AreEqual(1m, stateSplitter.CalcWeight(new MatchDetail { originDeadheadMiles = 0 })); ;
            Assert.AreEqual(0.5m, stateSplitter.CalcWeight(new MatchDetail { originDeadheadMiles = 50 }));
        }
    }
}
