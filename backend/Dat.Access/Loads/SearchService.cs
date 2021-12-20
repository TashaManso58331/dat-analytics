using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Text;
using Newtonsoft.Json;
using Dat.Access.Loads.Matches;
using Microsoft.Extensions.Caching.Memory;
using Dat.Access.Caches;

namespace Dat.Access.Loads
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> log;
        public HttpClient Client { get; }
        private readonly SimpleMemoryCache<Response.SearchResponse> searchResponseCache;

        public SearchService(ILogger<SearchService> log, HttpClient client, IMemoryCache memoryCache)
        { 
            this.log = log;
            client.BaseAddress = new Uri("https://freight.api.prod.dat.com/search/v1/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
            this.searchResponseCache = new SimpleMemoryCache<Response.SearchResponse>(memoryCache, log, nameof(Response.SearchResponse));
        }

        public async Task<Response.SearchResponse> GetSearchResponse(string acessToken, Request.SearchRequest searchCriteria)
        {
            try
            {
                var searchResponse = await Task.Run(() => searchResponseCache.GetOrCreate(searchCriteria.GetHashCode(), () => CreateSearch(acessToken, searchCriteria)));

                await searchResponseCache.SaveState();
                return searchResponse;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get searchResponse for searchRequest={0}", searchCriteria);
                throw;
            }
        }

        private async Task<Response.SearchResponse> CreateSearch(string acessToken, Request.SearchRequest searchCriteria)
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

        public async Task<List<MatchesResponse>> GetMatches(string acessToken, string searchId)
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acessToken);

                var response = await Client.GetAsync($"loads/{searchId}/matches?limit=500&lookbackMinutes=1");
                response.EnsureSuccessStatusCode();
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var matchesResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<List<MatchesResponse>>(responseStream);
                return matchesResponse;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get matches for {0}", searchId);
                throw;
            }
        }

    }
}
