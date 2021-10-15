using Dat.Model;

namespace Dat.Access.Clients
{
    public class DatClient : IDatClient
    {
        public AccessToken GetSessionToken(string sessionAccount, string sessionPassword)
        {
            throw new System.NotImplementedException();
        }

        public AccessToken GetUserToken(AccessToken sessionToken, string userAccount)
        {
            throw new System.NotImplementedException();
        }
    }
}
