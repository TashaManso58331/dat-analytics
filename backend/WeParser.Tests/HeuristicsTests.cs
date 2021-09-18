using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeParser.Tests
{
    [TestClass]
    public class HeuristicsTests
    {
        [TestMethod]
        public void Load()
        {
            var data = TestUtils.Utils.GetTestData();
            Assert.AreEqual(500, data.matchDetails.Count);
        }

        [TestMethod]
        public void Load2()
        {
            var data = TestUtils.Utils.GetTestData2();
            Assert.AreEqual(500, data.matchDetails.Count);
        }

        public void HeuristicTests()
        {
            var data = TestUtils.Utils.GetTestData2();
            Assert.AreEqual(500, data.matchDetails.Count);

        }
    }
}
