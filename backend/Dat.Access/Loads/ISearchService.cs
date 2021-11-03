using Dat.Access.Loads.Request;
using Dat.Access.Loads.Response;
using System.Threading.Tasks;

namespace Dat.Access.Loads
{
    public interface ISearchService
    {
        Task<SearchResponse> CreateSearch(string acessToken, SearchRequest searchCriteria);
    }
}
