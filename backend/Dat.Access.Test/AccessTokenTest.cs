using Dat.Model;
using NUnit.Framework;
using System;

namespace Dat.Access.Test
{
    [TestFixture]
    public class AccessTokenTest
    {
        [Test]
        public void ShouldParse()
        {
            var dateUtc = DateTime.UtcNow;
            var response = new AccessToken.Response { accessToken = "accessToken", expiresWhen = dateUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ") };

            var accessToken = AccessToken.From(response);

            Assert.AreEqual("accessToken", accessToken.Token);
            Assert.AreEqual(dateUtc, accessToken.Expiry.ToUniversalTime());
        }

        [Test]
        public void ShouldFailed()
        {
            var dateUtc = DateTime.UtcNow;

            var response = new AccessToken.Response { accessToken = "accessToken", expiresWhen = "Blah-Blah" };

            Assert.Throws<ArgumentException>(() => AccessToken.From(response));
        }
    }
}
