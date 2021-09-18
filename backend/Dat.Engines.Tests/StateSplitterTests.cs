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
    public class StateSplitterTests
    {
        private Model.States.Root States;

        [TestInitialize]
        public void SetUp()
        {
            States = Utils.GetTestStatesData();
            Assert.AreEqual(50, States.inboundOutbound.counts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyStates()
        {
            var stateSplitter = new StateSplitter(new Model.States.Root());
            stateSplitter.CalcWeight(new MatchDetail { destination = new Destination { state = "ANY" } });
        }

        [TestMethod]
        public void OneState()
        {
            var stateSplitter = new StateSplitter(new Model.States.Root() { inboundOutbound = new InboundOutbound { counts = new List<Count> { new Count { state="OR", outbound = 100} } } });
            Assert.AreEqual(1m, stateSplitter.CalcWeight(new MatchDetail { destination = new Destination { state = "OR" } }));
            Assert.AreEqual(0m, stateSplitter.CalcWeight(new MatchDetail { destination = new Destination { state = "ANY" } }));
        }

        [TestMethod]
        public void ManyState()
        {
            var stateSplitter = new StateSplitter(new Model.States.Root()
            {
                inboundOutbound = new InboundOutbound
                {
                    counts = new List<Count>
                    { new Count { state = "OR", outbound = 100 },
                    new Count { state = "TX", outbound = 50 },
                    new Count { state = "AR", outbound = 10 }
                    }
                }
            });
            Assert.AreEqual(1m, stateSplitter.CalcWeight(new MatchDetail { destination = new Destination { state = "OR" } }));
            Assert.AreEqual(0.5m, stateSplitter.CalcWeight(new MatchDetail { destination = new Destination { state = "TX" } }));
            Assert.AreEqual(0.1m, stateSplitter.CalcWeight(new MatchDetail { destination = new Destination { state = "AR" } }));
            Assert.AreEqual(0m, stateSplitter.CalcWeight(new MatchDetail { destination = new Destination { state = "ANY" } }));
        }
    }
}
