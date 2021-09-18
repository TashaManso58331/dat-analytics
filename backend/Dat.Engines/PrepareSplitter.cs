using Dat.Model.Offer;

namespace Dat.Engines
{
    public class PrepareSplitter : ISplitter
    {
        public string Name => nameof(PrepareSplitter);

        public Root Split(Root sourceData)
        {
            sourceData.matchDetails.RemoveAll(c=>c.rate <= 0);
            return sourceData;
        }
    }
}
