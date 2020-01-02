using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Profile;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Friends
{
    public partial class FriendsListViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;

        public FriendsListViewModel(
            IProfileService profileService)
        {
            _profileService = profileService;
        }

        public override async Task Initialize()
        {
            var userFriends = await _profileService.GetUserFriends();

            foreach (var friend in userFriends)
            {
                FriendsList.Add(friend);
            }
        }

        private async Task GoToFriendSearch()
        {
            throw new System.NotImplementedException();
        }

        private async Task ShowFriend(UserProfile arg)
        {
            await ShowViewModel<ProfileViewModel, ProfileUserModel>(new ProfileUserModel
            {
                UserId = arg.Id
            });
        }
    }
}
