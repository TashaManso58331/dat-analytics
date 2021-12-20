using System.Threading.Tasks;

namespace Dat.Access.Loads
{
    public interface ISearchService
    {
        Task<Response.SearchResponse> GetSearchResponse(string acessToken, Request.SearchRequest searchCriteria);
    }
}
