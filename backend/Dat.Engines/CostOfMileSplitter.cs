using Dat.Model.Offer;

namespace Dat.Engines
{
    public class CostOfMileSplitter : BaseSplitter
    {
        public override string Name => nameof(CostOfMileSplitter);

        public override decimal CalcWeight(MatchDetail c)
        {
            return (c.tripMiles == 0) ? c.rate : c.rate / c.tripMiles;
        }
    }
}
