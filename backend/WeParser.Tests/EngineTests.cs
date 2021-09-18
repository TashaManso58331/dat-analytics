using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebParser.Model;

namespace WeParser.Tests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public async Task Parse()
        {
            var engine = new WebParser.Parsers.Engine();
            var html = TestUtils.Utils.GetFileContents("TruckersEdge_01.html");
            Assert.IsFalse(string.IsNullOrEmpty(html));
            var result = await engine.ParseAsync(html);
            Assert.AreEqual(32, result.Count);

            var str = JsonConvert.SerializeObject(result[0]);
            var off1 = JsonConvert.DeserializeObject<Offer>(str);
            Assert.AreEqual(off1, result[0]);

            Assert.AreEqual(CreateFirstOffer(), result[0]);
            Assert.AreEqual(CreateLatestOffer(), result[result.Count-1]);
        }

        [TestMethod]
        public void LocationTest()
        {
            Assert.AreEqual(
                new Location { Name = "1", DeadHead = null },
                new Location { Name = "1", DeadHead = null }
                );
        }

        [TestMethod]
        public void OfferTest()
        {
            Assert.AreEqual(
                new Offer(),
                new Offer()
                );
        }


        private Offer CreateLatestOffer()
        {
            return JsonConvert.DeserializeObject<Offer>("{\"Origin\":{\"DeadHead\":148,\"Name\":\"Madison\",\"State\":\"WI\"},\"Destination\":{\"DeadHead\":null,\"Name\":\"Manteca\",\"State\":\"CA\"},\"Trip\":2057,\"Price\":500.0,\"Age\":\"10m\",\"Length\":{\"Value\":5,\"Unit\":\"ft\"},\"Weight\":{\"Value\":2500,\"Unit\":\"lbs\"},\"Company\":{\"Name\":\"Globaltranz/Lps, Scottsdale, AZ\",\"Contact\":\"(480) 291-6518\"}}");
        }

        private Offer CreateFirstOffer()
        {
            return JsonConvert.DeserializeObject<Offer>("{\"Origin\":{\"DeadHead\":94,\"Name\":\"Milwaukee\",\"State\":\"WI\"},\"Destination\":{\"DeadHead\":null,\"Name\":\"Woodland\",\"State\":\"CA\"},\"Trip\":2098,\"Price\":500.0,\"Age\":\"10m\",\"Length\":{\"Value\":5,\"Unit\":\"ft\"},\"Weight\":{\"Value\":2500,\"Unit\":\"lbs\"},\"Company\":{\"Name\":\"Globaltranz/Lps, Scottsdale, AZ\",\"Contact\":\"(480) 291-6518\"}}");
            
        }
    }
}
