using Dat.Access.Clients;
using Dat.Access.Controllers;
using Dat.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dat.Access.Test
{
    [TestFixture]
    public class AccessControllerTest
    {
        private Microsoft.Extensions.Logging.ILogger<AccessController> log = Mock.Of<Microsoft.Extensions.Logging.ILogger<AccessController>>();
        private IMemoryCache memoryCache = Mock.Of<IMemoryCache>();
        private IConfiguration config = Mock.Of<IConfiguration>();
        private IDatService datService = Mock.Of<IDatService>();
        private AccessToken goodSessionToken = AccessToken.CreateToken("session token", DateTime.Now.AddMinutes(5));
        private AccessToken goodUserToken = AccessToken.CreateToken("user token", DateTime.Now.AddMinutes(5));
        private ICacheEntry cachEntry = Mock.Of<ICacheEntry>();

        [SetUp]
        public void SetUp()
        {
            Mock.Get(config).Setup(p => p[DatService.cSessionAccount]).Returns("SessionAccount");
            Mock.Get(config).Setup(p => p[DatService.cSessionPassword]).Returns("SessionPassword");
            Mock.Get(config).Setup(p => p[DatService.cUserAccount]).Returns("UserAccount");
        }

        [Test]
        public void ShouldReturnAccessToken()
        {

            Mock.Get(datService).Setup(p => p.GetSessionToken()).Returns(Task.FromResult(goodSessionToken));
            Mock.Get(datService).Setup(p => p.GetUserToken(goodSessionToken, It.IsAny<string>())).Returns(Task.FromResult(goodUserToken));
            Mock.Get(memoryCache).Setup(p => p.CreateEntry((It.IsAny<object>()))).Returns(cachEntry);
            
            var controller = new AccessController(datService);
            var accessToken = controller.Get();

            Assert.False(string.IsNullOrEmpty(accessToken));
        }

        [Test]
        public void ShouldThrowOnSessionToken()
        {

            Mock.Get(datService).Setup(p => p.GetSessionToken()).Throws(new HttpRequestException());
            Mock.Get(datService).Setup(p => p.GetUserToken(goodSessionToken, It.IsAny<string>())).Returns(Task.FromResult(goodUserToken));
            Mock.Get(memoryCache).Setup(p => p.CreateEntry((It.IsAny<object>()))).Returns(cachEntry);

            var controller = new AccessController(datService);
            Assert.Throws<HttpRequestException>(() => controller.Get());
        }

        [Test]
        public void ShouldThrowOnUserToken()
        {

            Mock.Get(datService).Setup(p => p.GetSessionToken()).Returns(Task.FromResult(goodSessionToken)); 
            Mock.Get(datService).Setup(p => p.GetUserToken(goodSessionToken, It.IsAny<string>())).Throws(new HttpRequestException());
            Mock.Get(memoryCache).Setup(p => p.CreateEntry((It.IsAny<object>()))).Returns(cachEntry);

            var controller = new AccessController(datService);
            Assert.Throws<HttpRequestException>(() => controller.Get());
        }
    }
}
