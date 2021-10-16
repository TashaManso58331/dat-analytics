using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Dat.Model
{
    public class AccessToken : IAccessToken
    {
        public enum StateEnum { Success, Error};

        public readonly string Token;
        public readonly DateTime Expiry;
        public readonly StateEnum State;

        public static AccessToken CreateToken(string token, DateTime expiry) => new(token, expiry, StateEnum.Success);
        public static AccessToken CreateError(string message) => new(message, DateTime.Now, StateEnum.Error);

        private AccessToken(string token, DateTime expiry, StateEnum state)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            Expiry = expiry;
            State = state;
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
            throw new Exception($"Failed to parse repsonse={response}");
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
