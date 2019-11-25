using GamerHub.shared.Contracts.Requests;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.Services.Search
{
    public interface ISearchService
    {
        List<GameModelWithImage> GetSearchGamesModels(SearchGameRequest searchGameRequest);
    }
}
