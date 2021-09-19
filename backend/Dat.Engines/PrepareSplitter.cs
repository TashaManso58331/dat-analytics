using Dat.Model.Offer;
using Newtonsoft.Json;

namespace Dat.Engines
{
    public class PrepareSplitter : ISplitter
    {
        public string Name => nameof(PrepareSplitter);

        public Root Split(Root sourceData)
        {
            var copy = JsonConvert.DeserializeObject<Root>(JsonConvert.SerializeObject(sourceData));
            copy.matchDetails.RemoveAll(c=>c.rate <= 0);
            return copy;
        }
    }
}
