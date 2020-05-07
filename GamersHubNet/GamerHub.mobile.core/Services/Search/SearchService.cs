using System.Collections.Generic;
using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Http.Factory;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using RestSharp;

namespace GamerHub.mobile.core.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly IHttpClientFactoryService _httpClientFactoryService;
        private readonly IGlobalStateService _globalStateService;

        public SearchService(
            IHttpClientFactoryService httpClientFactoryService,
            IGlobalStateService globalStateService)
        {
            _httpClientFactoryService = httpClientFactoryService;
            _globalStateService = globalStateService;
        }

        public async Task<List<GameWithImageResponse>> GetSearchGamesModels(SearchGameRequest searchGameRequest)
        {
            var client = _httpClientFactoryService.GetHttpClient();

            var request = new RestRequest(ApiRoutes.Search.SearchGames)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("searchGameText", searchGameRequest.SearchGameText);
            request.AddQueryParameter("take", searchGameRequest.Take.ToString());
            request.AddQueryParameter("skip", searchGameRequest.Skip.ToString());

            var response = await client.ExecuteAsync<List<GameWithImageResponse>>(request);

            return response.ResponseData;
        }
    }
}
