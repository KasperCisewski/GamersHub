using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;

namespace GamerHub.mobile.core.Services.Game
{
    public interface IGameService
    {
        Task<List<GameModelWithImage>> GetGames(HomeGamesCategory homeGamesCategory);
        Task<FullDescriptionGameModel> GetFullGameModel(Guid gameId);
        Task AddGameToWishList(Guid gameId);
        Task AddGameToVault(Guid gameId);
        Task<List<ScreenShotModel>> GetScreenShotsForGame(Guid gameId);
        Task<List<PriceModel>> GetPricesModelsForGame(Guid gameId);
        Task<string> GetVideoUrlForGame(Guid gameId);
        Task<List<GameModelWithImage>> GetGamesByCategory(GameCategoryRequest gameCategoryRequest);
    }
}
