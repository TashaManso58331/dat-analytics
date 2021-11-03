using Dat.Model;
using System.Threading.Tasks;

namespace Dat.Access.Clients
{
    public interface IDatService
    {
        Task<AccessToken> GetSessionToken();
        Task<AccessToken> GetUserToken(AccessToken sessionToken, string userAccount);
        string GetAllTokens();
    }
}
