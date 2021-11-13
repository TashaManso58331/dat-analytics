using Dat.Access.Caches;
using Dat.Access.Clients;
using Dat.Access.Loads;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dat.Access.Test
{
    [TestFixture]
    class SearchServiceTest
    {
        private SearchService searchService;
        private Microsoft.Extensions.Logging.ILogger<SearchService> searchServiceLog = Mock.Of<Microsoft.Extensions.Logging.ILogger<SearchService>>();
        private Microsoft.Extensions.Logging.ILogger<DatService> datServiceLog = Mock.Of<Microsoft.Extensions.Logging.ILogger<DatService>>();
        private IConfiguration config = Mock.Of<IConfiguration>();
        private IDatService datClient;

         [SetUp]
        public void SetUp()
        {
            Mock.Get(config).Setup(p => p[DatService.cSessionAccount]).Returns("info@dieselmarket.net");
            Mock.Get(config).Setup(p => p[DatService.cSessionPassword]).Returns("BlahBlah");
            Mock.Get(config).Setup(p => p[DatService.cUserAccount]).Returns("dmitry.okatenko@gmail.com");

            var builder = new ConfigurationBuilder()
                .AddUserSecrets<SearchServiceTest>();

            var configuration = builder.Build();

            searchService = new SearchService(searchServiceLog, new HttpClient());
            datClient = new DatService(datServiceLog, new HttpClient(), new DatMemoryCache().Cache, configuration);

        }

        [Test]
        public async Task ShouldReturnSearchResponseIntegrationTest()
        {
            datClient.RestoreCachedTokens();
            var userToken = await datClient.GetAllTokens();
            Loads.Request.SearchRequest searchRequest = Loads.Request.SearchRequestBuilder.NewBuilder().TestBuild();
            var searchResponse = await searchService.CreateSearch(userToken, searchRequest);
            var matchResponse = await searchService.GetMatches(userToken, searchResponse.id);

            matchResponse = await searchService.GetMatches(userToken, searchResponse.id);
        }
    }
}
