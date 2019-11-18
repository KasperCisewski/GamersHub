using GamerHub.shared.Contracts.Requests;
using GamerHub.shared.Contracts.Responses;
using System.Collections.Generic;

namespace GamerHub.mobile.core.Services.Search
{
    public interface ISearchService
    {
        List<SearchGameResponse> GetSearchGamesModels(SearchGameRequest searchGameRequest);
    }
}
