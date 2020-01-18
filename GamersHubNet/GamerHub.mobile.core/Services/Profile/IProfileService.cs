using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.Services.Profile
{
    public interface IProfileService
    {
        Task<UserProfile> GetUserProfileInformation(Guid? userId);
        Task<List<UserProfile>> GetUserFriends();
        Task<List<UserProfile>> SearchUsers(SearchFriendsRequest searchFriendsRequest);
        Task<List<GameModelWithImage>> GetGamesInVault(Guid? userId);
        Task<List<GameModelWithImage>> GetWishListGames();
        Task<List<byte>> GetHeatMap(Guid? userId);
        Task<bool> DeleteFromFriendList(Guid userId);
        Task<bool> AddFriendToFriendList(Guid userId);
    }
}
