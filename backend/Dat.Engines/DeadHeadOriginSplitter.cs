using Dat.Model.Offer;
using System;

namespace Dat.Engines
{
    public class DeadHeadOriginSplitter : BaseSplitter
    {
        public override string Name => nameof(DeadHeadOriginSplitter);

        public override decimal CalcWeight(MatchDetail c)
        {
            return 1m - Math.Min((decimal)c.originDeadheadMiles, 100m) / 100m;
        }
    }
}
