using Dat.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dat.Engines.Tests
{
    [TestClass]
    public class PrepareSplitterTests
    {
        [TestMethod]
        public void Load()
        {
            var sourceData = Utils.GetTestOfferData();

            var splitter = new Dat.Engines.PrepareSplitter();
            var preparedData = splitter.Split(sourceData);
            Assert.AreEqual(215, preparedData.matchDetails.Count);
            Assert.AreEqual(500, sourceData.matchDetails.Count);
        }

        [TestMethod]
        public void LoadStates()
        {
            var states = Utils.GetTestStatesData();
            Assert.AreEqual(50, states.inboundOutbound.counts.Count);
        }
    }
}
