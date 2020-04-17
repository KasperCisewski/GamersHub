using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<UserProfileResponse> GetUserProfileInformation(Guid? userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.ProfileRoot)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("userId", userId?.ToString());

            var response = await client.ExecuteAsync<UserProfileResponse>(request);

            return response.ResponseData;
        }

        public async Task<List<UserProfileResponse>> GetUserFriends()
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetUserFriends)
            {
                Method = Method.GET
            };

            var response = await client.ExecuteAsync<List<UserProfileResponse>>(request);

            return response.ResponseData;
        }

        public async Task<List<UserProfileResponse>> SearchUsers(SearchUserRequest searchUserRequest)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Search.SearchUsers)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("searchUserNameText", searchUserRequest.SearchUserNameText);
            request.AddQueryParameter("take", searchUserRequest.Take.ToString());
            request.AddQueryParameter("skip", searchUserRequest.Skip.ToString());

            var response = await client.ExecuteAsync<List<UserProfileResponse>>(request);

            return response.ResponseData;
        }

        public async Task<List<GameWithImageResponse>> GetGamesInVault(Guid? userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetGamesInVault)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("userId", userId?.ToString());

            var response = await client.ExecuteAsync<List<GameWithImageResponse>>(request);

            return response.ResponseData;
        }

        public async Task<List<GameWithImageResponse>> GetWishListGames()
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.GetWishListGames)
            {
                Method = Method.GET
            };

            var response = await client.ExecuteAsync<List<GameWithImageResponse>>(request);

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

            var request = new RestRequest(ApiRoutes.Profile.UserFriendsRoot)
            {
                Method = Method.DELETE
            };

            request.AddJsonBody(new
            {
                UserId = userId
            });

            var response = await client.ExecuteAsync(request);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> AddFriendToFriendList(Guid userId)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.UserFriendsRoot)
            {
                Method = Method.POST
            };

            request.AddJsonBody(new
            {
                UserId = userId
            });

            var response = await client.ExecuteAsync(request);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> EditProfileImage(byte[] bytes)
        {
            var client = _httpClientFactoryService.GetAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Profile.ChangeProfileImage)
            {
                Method = Method.POST
            };

            request.AddJsonBody(new
            {
                Bytes = bytes.ToList()
            });

            var response = await client.ExecuteAsync(request);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
