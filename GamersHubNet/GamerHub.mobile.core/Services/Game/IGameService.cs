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
        Task<List<GameWithImageResponse>> GetGames(HomeGamesCategory homeGamesCategory);
        Task<FullGameDescriptionResponse> GetFullGameModel(Guid gameId);
        Task<bool> AddGameToWishList(Guid gameId);
        Task<bool> AddGameToVault(Guid gameId);
        Task<bool> DeleteGameFromVault(Guid gameModelId);
        Task<bool> DeleteGameFromWishList(Guid gameModelId);
        Task<List<ScreenShotResponse>> GetScreenShotsForGame(Guid gameId);
        Task<List<GameOfferResponse>> GetPricesModelsForGame(Guid gameId);
        Task<string> GetVideoUrlForGame(Guid gameId);
        Task<List<GameWithImageResponse>> GetGamesByCategory(GameCategoryRequest gameCategoryRequest);
        Task<List<GameWithImageResponse>> GetGamesForUser();
    }
}
