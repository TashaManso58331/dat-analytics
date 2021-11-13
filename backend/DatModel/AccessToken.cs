using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Dat.Model
{
    public class AccessToken : IAccessToken
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }

        public static AccessToken CreateToken(string token, DateTime expiry) => new(token, expiry);

        public AccessToken() { }

        private AccessToken(string token, DateTime expiry)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            Expiry = expiry;
        }

        public DateTime GetExpiry()
        {
            return Expiry;
        }

        public static AccessToken From(Response response)
        {
            if (DateTime.TryParse(response.expiresWhen, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime parsedExpiresWhen))
            {
                return CreateToken(response.accessToken, parsedExpiresWhen);
            }
            throw new ArgumentException($"Failed to parse repsonse={response}");
        }

        public class Response
        {
            public string accessToken { get; set; }
            public string expiresWhen { get; set; }

            public override string ToString()
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this);
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
