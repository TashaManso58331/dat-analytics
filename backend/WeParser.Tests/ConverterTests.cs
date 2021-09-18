using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebParser.Utils;

namespace WeParser.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void CheckIntParse()
        {
            int.TryParse("1,111", NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out int value);
            Assert.AreEqual(1111, value);
        }

        [TestMethod]
        public void CheckPriceParse()
        {
            decimal.TryParse("$500", NumberStyles.Currency, new CultureInfo("en-US"), out decimal value);
            Assert.AreEqual(500m, value);
        }
    }
}
