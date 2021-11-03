using Dat.Model;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using static Dat.Model.AccessToken;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Dat.Access.Caches;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;


namespace Dat.Access.Clients
{
    public class DatService : IDatService
    {
        private readonly ILogger<DatService> log;
        private readonly SimpleMemoryCache<AccessToken> serviceTokenCache;
        private readonly SimpleMemoryCache<AccessToken> userTokenCache;
        private readonly string sessionAccount;
        private readonly string sessionPassword;
        private readonly string userAccount;

        public const string cSessionAccount = "Dat:SessionAccount";
        public const string cSessionPassword = "Dat:SessionPassword";
        public const string cUserAccount = "Dat:UserAccount";

        public HttpClient Client { get; }

        public DatService(ILogger<DatService> log, HttpClient client, IMemoryCache memoryCache, IConfiguration config)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            
            client.BaseAddress = new Uri("https://identity.api.dat.com/access/v1/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
            this.serviceTokenCache = new SimpleMemoryCache<AccessToken>(memoryCache);
            this.userTokenCache = new SimpleMemoryCache<AccessToken>(memoryCache);
            this.sessionAccount = config[cSessionAccount] ?? throw new ArgumentNullException(cSessionAccount);
            this.sessionPassword = config[cSessionPassword] ?? throw new ArgumentNullException(cSessionPassword);
            this.userAccount = config[cUserAccount] ?? throw new ArgumentNullException(cUserAccount);
        }

        [return: NotNull]
        public async Task<AccessToken> GetSessionToken()
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization = null;
                var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new { username = sessionAccount, password = sessionPassword });
                var response = await Client.PostAsync("token/organization", new StringContent(requestBody, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var parsedResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<Response>(responseStream);

                return AccessToken.From(parsedResponse);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get session token for {0}", sessionAccount);
                throw;
            }
        }

        [return: NotNull]
        public async Task<AccessToken> GetUserToken([NotNull] AccessToken sessionToken, [NotNull] string userAccount)
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken.Token);

                var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new { username = userAccount });
                var response = await Client.PostAsync("token/user", new StringContent(requestBody, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var parsedResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<Response>(responseStream);

                return AccessToken.From(parsedResponse);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get session token for {0}", userAccount);
                throw;
            }
        }

        public string GetAllTokens()
        {
            try
            {
                var sessionToken = serviceTokenCache.GetOrCreate(sessionAccount, () => this.GetSessionToken());
                var userToken = userTokenCache.GetOrCreate(userAccount, () => this.GetUserToken(sessionToken, userAccount));
                return userToken?.Token;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get user token sessionAccount={0} userAccount={1}");
                throw;
            }
        }
    }
}
