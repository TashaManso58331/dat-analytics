using System;

namespace Dat.Model
{
    public class AccessToken
    {
        public readonly string Token;
        public readonly DateTime Expiry;

        public AccessToken(string token, DateTime expiry)
        {
            Token = token;
            Expiry = expiry;
        }
    }
}
