using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Text;
using Newtonsoft.Json;
using Dat.Access.Loads.Response;

namespace Dat.Access.Loads
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> log;
        public HttpClient Client { get; }

        public SearchService(ILogger<SearchService> log, HttpClient client)
        { 
            this.log = log;
            client.BaseAddress = new Uri("https://freight.api.prod.dat.com/search/v1/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }

        public async Task<SearchResponse> CreateSearch(string acessToken, Request.SearchRequest searchCriteria)
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acessToken);
                var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(searchCriteria, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                var response = await Client.PostAsync("loads", new StringContent(requestBody, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var searchResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<Dat.Access.Loads.Response.SearchResponse>(responseStream);
                return searchResponse;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get loads for {0}", searchCriteria);
                throw;
            }
        }
    }
}
