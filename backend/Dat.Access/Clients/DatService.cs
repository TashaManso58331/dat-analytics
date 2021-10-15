using Dat.Model;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            Client = client;
        }

        public async Task<AccessToken> GetSessionToken(string sessionAccount, string sessionPassword)
        {
            try
            {
                var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new { username = sessionAccount, password = sessionPassword });
                var response = await Client.PostAsync("/token/organization", new StringContent(requestBody));
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

        public AccessToken GetUserToken(AccessToken sessionToken, string userAccount)
        {
            throw new System.NotImplementedException();
        }
    }
}
