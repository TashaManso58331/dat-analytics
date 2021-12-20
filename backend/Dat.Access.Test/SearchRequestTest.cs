using Dat.Access.Loads.Request;
using NUnit.Framework;
using System;

namespace Dat.Access.Test
{
    [TestFixture]
    class SearchRequestTest
    {
        [Test]
        public void CheckEquals()
        {
            var t1 = new SearchRequest();
            var t2 = new SearchRequest();

            Assert.AreEqual(t1, t2);
        }

        [Test]
        public void CheckTestEquals()
        {
            DateTime utcNow = DateTime.UtcNow;
            var t1 = Loads.Request.SearchRequestBuilder.NewBuilder().TestBuild(utcNow).GetHashCode();
            var t2 = Loads.Request.SearchRequestBuilder.NewBuilder().TestBuild(utcNow).GetHashCode();

            Assert.AreEqual(t1, t2);
        }
    }
}
