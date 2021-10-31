using Dat.Model;
using System.Threading.Tasks;

namespace Dat.Access.Clients
{
    public interface IDatService
    {
        Task<AccessToken> GetSessionToken(string sessionAccount, string sessionPassword);
        Task<AccessToken> GetUserToken(AccessToken sessionToken, string userAccount);
        string GetAllTokens();
    }
}
