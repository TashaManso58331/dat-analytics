using Dat.Model;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using static Dat.Model.AccessToken;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Dat.Access.Clients
{
    public class DatService : IDatService
    {
        private readonly ILogger<DatService> log;
        public HttpClient Client { get; }

        public DatService(ILogger<DatService> log, HttpClient client)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            
            client.BaseAddress = new Uri("https://identity.api.dat.com/access/v1/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }

        [return: NotNull]
        public async Task<AccessToken> GetSessionToken([NotNull] string sessionAccount, [NotNull] string sessionPassword)
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization = null;
                var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new { username = sessionAccount, password = sessionPassword });
                var response = await Client.PostAsync("token/organization", new StringContent(requestBody, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var parsedResponse = await JsonSerializer.DeserializeAsync<Response>(responseStream);

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
                var parsedResponse = await JsonSerializer.DeserializeAsync<Response>(responseStream);

                return AccessToken.From(parsedResponse);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to get session token for {0}", userAccount);
                throw;
            }
        }
    }
}
