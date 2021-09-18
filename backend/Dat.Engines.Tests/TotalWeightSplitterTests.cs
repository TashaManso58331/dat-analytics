using Dat.Model.Offer;
using Dat.Model.States;
using Dat.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System;

namespace Dat.Engines.Tests
{
    [TestClass]
    public class TotalWeightSplitterTests
    {
        [TestMethod]
        public void EmptyStates()
        {
            var totalWeightSplitter = new TotalWeightSplitter();
            var matchDetail = new MatchDetail();
            Assert.AreEqual(0m, totalWeightSplitter.CalcWeight(matchDetail));
        }


        [TestMethod]
        public void ManyState()
        {
            var totalWeightSplitter = new TotalWeightSplitter();
            var matchDetail = new MatchDetail();
            matchDetail.Weights["Splitter1"] = 1.0m;
            matchDetail.Weights["Splitter2"] = 0.8m;
            matchDetail.Weights["Splitter3"] = 0.7m;
            Assert.AreEqual(0.56m, totalWeightSplitter.CalcWeight(matchDetail));
        }
    }
}
