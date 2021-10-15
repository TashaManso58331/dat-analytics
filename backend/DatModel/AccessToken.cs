using System;

namespace Dat.Model
{
    public class AccessToken : IAccessToken
    {
        public static AccessToken ErrorAccessToken = new AccessToken(String.Empty, DateTime.Now, StateEnum.Error);

        public enum StateEnum { Success, Error};

        public readonly string Token;
        public readonly DateTime Expiry;
        public readonly StateEnum State;

        public static AccessToken Create(string token, DateTime expiry) => new(token, expiry, StateEnum.Success);

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
    }
}
