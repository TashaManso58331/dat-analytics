using Dat.Model.Offer;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Dat.Engines
{
    [ContractClassFor(typeof(StateSplitter))]
    public class StateSplitter: BaseSplitter
    {
        [NotNull]
        private readonly Dictionary<string, decimal> StateWeights;

        public StateSplitter([NotNull] Model.States.Root states)
        {
            if (states.inboundOutbound == null || states.inboundOutbound.counts == null)
                throw new ArgumentNullException("states contains null data");

            var max = states.inboundOutbound.counts.Max(c => c.outbound);
            StateWeights = states.inboundOutbound.counts.ToDictionary(c => c.state, c => (decimal)c.outbound / max );
        }

        public override string Name => nameof(StateSplitter);

        public override decimal CalcWeight(MatchDetail c)
        {
            string state = c.destination?.state;
            if (string.IsNullOrEmpty(state))
                return 0;
            if (StateWeights.TryGetValue(state, out decimal stateWeight))
            {
                return stateWeight;
            }
            return 0;
        }
    }
}
