using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.Services.Profile
{
    public interface IProfileService
    {
        Task<UserProfileResponse> GetUserProfileInformation(Guid? userId);
        Task<List<UserProfileResponse>> GetUserFriends();
        Task<List<UserProfileResponse>> SearchUsers(SearchUserRequest searchUserRequest);
        Task<List<GameWithImageResponse>> GetGamesInVault(Guid? userId);
        Task<List<GameWithImageResponse>> GetWishListGames();
        Task<List<byte>> GetHeatMap(Guid? userId);
        Task<bool> DeleteFromFriendList(Guid userId);
        Task<bool> AddFriendToFriendList(Guid userId);
    }
}
