using Dat.Model.Offer;

namespace Dat.Engines
{
    public class CostOfMileSplitter : BaseSplitter
    {
        public const decimal cMinTrip = 300m;

        public override string Name => nameof(CostOfMileSplitter);

        private readonly decimal minTrip;

        public CostOfMileSplitter() : this(cMinTrip) { }        

        public CostOfMileSplitter(decimal minTrip)
        {
            this.minTrip = minTrip;
        }

        public override decimal CalcWeight(MatchDetail c)
        {
            if (c.tripMiles < minTrip)
            {
                return 0m;
            }
            return c.rate / c.tripMiles;
        }
    }
}
