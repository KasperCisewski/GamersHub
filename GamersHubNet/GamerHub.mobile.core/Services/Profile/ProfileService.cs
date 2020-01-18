using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Http.Factory;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using RestSharp;

namespace GamerHub.mobile.core.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpClientFactoryService _httpClientFactoryService;

        public ProfileService(
            IHttpClientFactoryService httpClientFactoryService)
        {
            _httpClientFactoryService = httpClientFactoryService;
        }

        public async Task<UserProfile> GetUserProfileInformation(Guid? userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetUserProfileInformation)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("userId", userId?.ToString());

            var response = await client.ExecuteAsync<UserProfile>(request);

            return response.ResponseData;
        }

        public async Task<List<UserProfile>> GetUserFriends()
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetUserFriends)
            {
                Method = Method.GET
            };

            var response = await client.ExecuteAsync<List<UserProfile>>(request);

            return response.ResponseData;
        }

        public async Task<List<UserProfile>> SearchUsers(SearchFriendsRequest searchFriendsRequest)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.SearchUsers)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("searchUserNameText", searchFriendsRequest.SearchUserNameText);
            request.AddQueryParameter("take", searchFriendsRequest.Take.ToString());
            request.AddQueryParameter("skip", searchFriendsRequest.Skip.ToString());

            var response = await client.ExecuteAsync<List<UserProfile>>(request);

            return response.ResponseData;
        }

        public async Task<List<GameModelWithImage>> GetGamesInVault(Guid? userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetGamesInVault)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("userId", userId?.ToString());

            var response = await client.ExecuteAsync<List<GameModelWithImage>>(request);

            return response.ResponseData;
        }

        public async Task<List<GameModelWithImage>> GetWishListGames()
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetWishListGames)
            {
                Method = Method.GET
            };

            var response = await client.ExecuteAsync<List<GameModelWithImage>>(request);

            return response.ResponseData;
        }

        public async Task<List<byte>> GetHeatMap(Guid? userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetHeatMap)
            {
                Method = Method.GET
            };

            request.AddQueryParameter("userId", userId?.ToString());

            var response = await client.ExecuteAsync<List<byte>>(request);

            return response.ResponseData;
        }

        public async Task<bool> DeleteFromFriendList(Guid userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.DeleteFromFriendList)
            {
                Method = Method.POST
            };

            request.AddQueryParameter("userId", userId.ToString());

            var response = await client.ExecuteAsync<bool>(request);

            return response.ResponseData;
        }

        public async Task<bool> AddFriendToFriendList(Guid userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.AddToFriendList)
            {
                Method = Method.POST
            };

            request.AddQueryParameter("userId", userId.ToString());

            var response = await client.ExecuteAsync<bool>(request);

            return response.ResponseData;
        }
    }
}
