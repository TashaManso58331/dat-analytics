using Dat.Model.Offer;

namespace Dat.Engines
{
    public class TotalCostSplitter : BaseSplitter
    {
        public const decimal cMinCost = 999m;

        private readonly decimal MinCost;

        public override string Name => nameof(TotalCostSplitter);

        public TotalCostSplitter() : this(cMinCost) { }

        public TotalCostSplitter(decimal minCost)
        {
            this.MinCost = minCost;
        }

        public override decimal CalcWeight(MatchDetail c)
        {
            return (c.rate < this.MinCost) ? 0 : c.rate;
        }
    }
}
