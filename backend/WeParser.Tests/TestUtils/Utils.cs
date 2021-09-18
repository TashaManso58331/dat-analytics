using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using WebParser.Model.Dat;

namespace WeParser.Tests.TestUtils
{
    class Utils
    {
        public static string GetFileContents(string sampleFile)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resource = string.Format("WeParser.Tests.Data.{0}", sampleFile);
            using (var stream = asm.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
            throw new FileNotFoundException("Failed to load test resources from missed file", resource);
        }

        private static Root InternalGetTestData(string fileName)
        {
            var jsonResponse = TestUtils.Utils.GetFileContents(fileName);
            return JsonConvert.DeserializeObject<Root>(jsonResponse);
        }

        public static Root GetTestData()
        {
            return InternalGetTestData("Response.json");
        }

        public static Root GetTestData2()
        {
            return InternalGetTestData("Response_02.json");
        }
    }
}
