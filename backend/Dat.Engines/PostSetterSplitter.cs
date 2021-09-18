using Dat.Model.Offer;

namespace Dat.Engines
{
    public class PostSetterSplitter : ISplitter
    {
        public string Name => nameof(PrepareSplitter);

        public Root Split(Root sourceData)
        {
            sourceData.matchDetails.ForEach(c =>
            {
                c.CostOfMileSplitter = TryGetValueOrDefault(c, nameof(CostOfMileSplitter));
                c.StateSplitter = TryGetValueOrDefault(c, nameof(StateSplitter));
                c.TotalCostSplitter = TryGetValueOrDefault(c, nameof(TotalCostSplitter));
                c.TotalWeightSplitter = TryGetValueOrDefault(c, nameof(TotalWeightSplitter));
                c.DeadHeadOriginSplitter = TryGetValueOrDefault(c, nameof(DeadHeadOriginSplitter));
            });
            return sourceData;
        }

        private decimal TryGetValueOrDefault(MatchDetail c, string key)
        {
            if (c.Weights.TryGetValue(key, out decimal value))
            {
                return value;
            }
            return 0m;
        }
    }
}
