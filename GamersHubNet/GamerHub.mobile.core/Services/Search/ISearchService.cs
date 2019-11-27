﻿using GamerHub.shared.Contracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.Services.Search
{
    public interface ISearchService
    {
        Task<List<GameModelWithImage>> GetSearchGamesModels(SearchGameRequest searchGameRequest);
    }
}
