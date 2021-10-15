using Dat.Model;

namespace Dat.Access.Clients
{
    public interface IDatClient
    {
        AccessToken GetSessionToken(string sessionAccount, string sessionPassword);
        AccessToken GetUserToken(AccessToken sessionToken, string userAccount);
    }
}
