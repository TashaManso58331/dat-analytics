using System;

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

        public static AccessToken From(Response parsedResponse)
        {
            DateTime.TryParse(parsedResponse.expiresWhen, 

            return CreateToken(parsedResponse.accessToken, )
            return new AccessToken
            {
                Token = parsedResponse.accessToken,
                State = StateEnum.Success,
                
                
            };
        }

        public class Response
        {
            public string accessToken { get; set; }
            public DateTime expiresWhen { get; set; }
        }
    }
}
