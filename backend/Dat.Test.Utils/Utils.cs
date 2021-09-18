using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace Dat.Test
{
    public class Utils
    {
        public static string GetFileContents(string sampleFile)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resource = string.Format("Dat.Test.Utils.Data.{0}", sampleFile);
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

        private static T InternalGetTestData<T>(string fileName)
        {
            var jsonResponse = GetFileContents(fileName);
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public static Model.Offer.Root GetTestOfferData()
        {
            return InternalGetTestData<Model.Offer.Root>("Response.json");
        }
        public static Model.States.Root GetTestStatesData()
        {
            return InternalGetTestData<Model.States.Root>("States.json");
        }
    }
}
