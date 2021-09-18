using Dat.Model.Offer;
using System.Linq;

namespace Dat.Engines
{
    public class TotalWeightSplitter : BaseSplitter
    {
        public override string Name => nameof(TotalWeightSplitter);

        public override decimal CalcWeight(MatchDetail c)
        {
            if (c.Weights.Count == 0)
                return 0m;
            return c.Weights.Select(c=>c.Value).Aggregate((s, a) => s * a);            
        }
    }
}
