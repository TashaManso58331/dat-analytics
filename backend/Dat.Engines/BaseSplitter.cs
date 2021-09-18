using Dat.Model.Offer;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Dat.Engines
{
    public abstract class BaseSplitter: ISplitter
    {
        private const decimal EPSILON = (decimal)1e-7;
        public virtual string Name => throw new NotImplementedException();

        public Root Split(Root sourceData)
        {
            var maxRate = this.Max(sourceData);
            sourceData.matchDetails.ForEach(c => UpdateWeight(c, Name, CalcWeight(c) / maxRate));
            return sourceData;
        }

        public virtual decimal CalcWeight(MatchDetail c)
        {
            throw new NotImplementedException();
        }

        private decimal Max(Root sourceData)
        {
            var maxValue = sourceData.matchDetails.Max(c => this.CalcWeight(c));
            Contract.Ensures(maxValue > EPSILON, $"MAX value must be greater that 0 for {Name}");
            return maxValue;
        }
        private void UpdateWeight(MatchDetail c, string splitterName, decimal weight)
        {
            c.Weights[splitterName] = weight;
        }
    }
}
