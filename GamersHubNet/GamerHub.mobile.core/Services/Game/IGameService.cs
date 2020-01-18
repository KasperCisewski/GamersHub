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
        Task<bool> AddGameToWishList(Guid gameId);
        Task<bool> AddGameToVault(Guid gameId);
        Task<bool> DeleteGameFromVault(Guid gameModelId);
        Task<bool> DeleteGameFromWishList(Guid gameModelId);
        Task<List<ScreenShotModel>> GetScreenShotsForGame(Guid gameId);
        Task<List<PriceModel>> GetPricesModelsForGame(Guid gameId);
        Task<string> GetVideoUrlForGame(Guid gameId);
        Task<List<GameModelWithImage>> GetGamesByCategory(GameCategoryRequest gameCategoryRequest);
        Task<List<GameModelWithImage>> GetGamesForUser();
    }
}
