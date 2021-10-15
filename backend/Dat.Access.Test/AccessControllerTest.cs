using Dat.Access.Clients;
using Dat.Access.Controllers;
using Dat.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;

namespace Dat.Access.Test
{
    [TestFixture]
    public class AccessControllerTest
    {
        private Mock<Microsoft.Extensions.Logging.ILogger<AccessController>> log = new Mock<Microsoft.Extensions.Logging.ILogger<AccessController>>();
        private Mock<IMemoryCache> memoryCache = new Mock<IMemoryCache>();
        private Mock<IConfiguration> config = new Mock<IConfiguration>();
        private Mock<IDatService> datClient = new Mock<IDatService>();
        private AccessToken goodSessionToken = AccessToken.CreateToken("session token", DateTime.Now.AddMinutes(5));
        private AccessToken goodUserToken = AccessToken.CreateToken("user token", DateTime.Now.AddMinutes(5));

        [SetUp]
        public void SetUp()
        {
            config.Setup(p => p[AccessController.cSessionAccount]).Returns("SessionAccount");
            config.Setup(p => p[AccessController.cSessionPassword]).Returns("SessionPassword");
            config.Setup(p => p[AccessController.cUserAccount]).Returns("UserAccount");
        }

        [Test]
        public void ShouldReturnAccessToken()
        {

            datClient.Setup(p => p.GetSessionToken(It.IsAny<string>(), It.IsAny<string>())).Returns(goodSessionToken);
            datClient.Setup(p => p.GetUserToken(goodSessionToken, It.IsAny<string>())).Returns(goodUserToken);
            memoryCache.Setup(p => p.Set<AccessToken>("SessionAccount", goodSessionToken, It.IsAny<MemoryCacheEntryOptions>())).Returns(goodSessionToken);
            memoryCache.Setup(p => p.Set<AccessToken>("UserAccount", goodUserToken, It.IsAny<MemoryCacheEntryOptions>())).Returns(goodUserToken);

            var controller = new AccessController(log.Object, memoryCache.Object, config.Object, datClient.Object);
            var accessToken = controller.Get();
            Assert.IsTrue(accessToken.State == Model.AccessToken.StateEnum.Success);
        }
    }
}
