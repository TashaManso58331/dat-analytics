using Dat.Model.Offer;
using Dat.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Dat.Engines.Tests
{
    [TestClass]
    public class PostSetterSplitterTests
    {
        [TestMethod]
        public void Load()
        {
            var splitter = new PostSetterSplitter();
            var matchDetail1 = new MatchDetail();
            var matchDetail2 = new MatchDetail();
            matchDetail1.Weights[nameof(CostOfMileSplitter)] = 0.9m;
            matchDetail2.Weights[nameof(TotalWeightSplitter)] = 0.8m;

            Root sourceData = new Root { matchDetails = new List<MatchDetail> { matchDetail1, matchDetail2} };
            var postData = splitter.Split(sourceData);

            Assert.AreEqual(0.9m, matchDetail1.CostOfMileSplitter);
            Assert.AreEqual(0m, matchDetail1.TotalWeightSplitter);

            Assert.AreEqual(0m, matchDetail2.CostOfMileSplitter);
            Assert.AreEqual(0.8m, matchDetail2.TotalWeightSplitter);
        }
        [TestMethod]
        public void CheckAll()
        {
            var matchDetail = new MatchDetail();
            Random rnd = new Random();
            foreach (var splitterName in new List<string> { 
                nameof(CostOfMileSplitter), nameof(TotalWeightSplitter), nameof(StateSplitter), nameof(TotalCostSplitter), nameof(DeadHeadOriginSplitter) })
            {
                matchDetail.Weights[splitterName] = (decimal)rnd.NextDouble();
            }

            var splitter = new PostSetterSplitter();
            var result = splitter.Split(new Root { matchDetails = new List<MatchDetail> { matchDetail } });

            Assert.AreEqual(matchDetail.Weights[nameof(CostOfMileSplitter)], matchDetail.CostOfMileSplitter, nameof(CostOfMileSplitter));
            Assert.AreEqual(matchDetail.Weights[nameof(TotalWeightSplitter)], matchDetail.TotalWeightSplitter, nameof(TotalWeightSplitter));
            Assert.AreEqual(matchDetail.Weights[nameof(StateSplitter)], matchDetail.StateSplitter, nameof(StateSplitter));
            Assert.AreEqual(matchDetail.Weights[nameof(TotalCostSplitter)], matchDetail.TotalCostSplitter, nameof(TotalCostSplitter));
            Assert.AreEqual(matchDetail.Weights[nameof(DeadHeadOriginSplitter)], matchDetail.DeadHeadOriginSplitter, nameof(DeadHeadOriginSplitter));
        }
    }
}
