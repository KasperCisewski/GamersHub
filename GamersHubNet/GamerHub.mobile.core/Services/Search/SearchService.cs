using System.Collections.Generic;
using System.Threading.Tasks;
using GamerHub.shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.Services.Search
{
    public class SearchService : ISearchService
    {
        public Task<List<GameModelWithImage>> GetSearchGamesModels(SearchGameRequest searchGameRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
