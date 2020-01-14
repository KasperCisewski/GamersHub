using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Http.Factory;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using RestSharp;

namespace GamerHub.mobile.core.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IHttpClientFactoryService _httpClientFactoryService;

        public GameService(
            IHttpClientFactoryService httpClientFactoryService)
        {
            _httpClientFactoryService = httpClientFactoryService;
        }

        public async Task<List<GameModelWithImage>> GetGames(HomeGamesCategory homeGamesCategory)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.GetGamesForHomeScreen)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("homeGamesCategory", homeGamesCategory.ToString());

            var response = await client.ExecuteAsync<List<GameModelWithImage>>(request);

            return response.ResponseData;
        }

        public async Task<FullDescriptionGameModel> GetFullGameModel(Guid gameId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.GetFullGameDescription)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("gameId", gameId.ToString());

            var response = await client.ExecuteAsync<FullDescriptionGameModel>(request);

            return response.ResponseData;
        }

        public async Task<bool> AddGameToWishList(Guid gameId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.AddGameToWishList)
            {
                Method = Method.POST
            };
            request.AddQueryParameter("gameId", gameId.ToString());

            var response = await client.ExecuteAsync(request);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> AddGameToVault(Guid gameId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.AddGameToVault)
            {
                Method = Method.POST
            };
            request.AddQueryParameter("gameId", gameId.ToString());

            await client.ExecuteAsync(request);

            var response = await client.ExecuteAsync(request);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<List<ScreenShotModel>> GetScreenShotsForGame(Guid gameId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.GetScreenshotsForGame)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("gameId", gameId.ToString());

            var response = await client.ExecuteAsync<List<ScreenShotModel>>(request);

            return response.ResponseData;
        }

        public async Task<List<PriceModel>> GetPricesModelsForGame(Guid gameId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.GetPricesForGame)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("gameId", gameId.ToString());

            var response = await client.ExecuteAsync<List<PriceModel>>(request);

            return response.ResponseData;
        }

        public async Task<string> GetVideoUrlForGame(Guid gameId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.GetVideoUrl)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("gameId", gameId.ToString());

            var response = await client.ExecuteAsync<string>(request);

            return response.ResponseData;
        }

        public async Task<List<GameModelWithImage>> GetGamesByCategory(GameCategoryRequest gameCategoryRequest)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Games.GetGamesByCategory)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("gameCategory", gameCategoryRequest.GameCategory.ToString());
            request.AddQueryParameter("take", gameCategoryRequest.Take.ToString());
            request.AddQueryParameter("skip", gameCategoryRequest.Skip.ToString());

            var response = await client.ExecuteAsync<List<GameModelWithImage>>(request);

            return response.ResponseData;
        }
    }
}
