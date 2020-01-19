using System.Linq;
using System.Threading.Tasks;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.Services.Resource;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.GamesVault;
using GamerHub.mobile.core.ViewModels.CoreApp.Settings;
using GamerHub.mobile.core.ViewModels.CoreApp.WishList;
using FriendsListViewModel = GamerHub.mobile.core.ViewModels.CoreApp.Friends.FriendsListViewModel;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Profile
{
    public partial class ProfileViewModel : BaseViewModel<ProfileUserModel>
    {
        private readonly IProfileService _profileService;
        private readonly IResourceService _resourceService;

        public ProfileViewModel(
            IProfileService profileService,
            IResourceService resourceService)
        {
            _profileService = profileService;
            _resourceService = resourceService;
        }

        public override void Prepare(ProfileUserModel parameter)
        {
            IsOtherUserProfile = parameter.UserId != null;
            UserId = IsOtherUserProfile ? parameter.UserId : null;
            IsVisibleBackButton = IsOtherUserProfile;
        }

        public override async Task Initialize()
        {
            var model = await _profileService.GetUserProfileInformation(_userId);
            UserName = model.UserName;
            IsUserFriend = model.IsUserFriend;
            //TODO
            if (model.ProfileImageContent != null && model.ProfileImageContent.Any())
            {
                ProfileImageBitmap = BitmapFactory.DecodeByteArray(model.ProfileImageContent.ToArray(), 0, model.ProfileImageContent.Count);
            }
            else
            {
                var profileImageId = _resourceService.GetDrawableId("ProfileImage");
                var resource = _resourceService.GetResources();

                ProfileImageBitmap = BitmapFactory.DecodeResource(resource, profileImageId);
            }
        }

        private async Task AddToFriendList()
        {
            var result = await _profileService.AddFriendToFriendList(UserId.Value);

            if (result)
            {
                IsUserFriend = true;
                NotificationService.Notify($"Successfully added {UserName} to friend list");
            }
            else
            {
                NotificationService.Notify( "Something was wrong");
            }
        }

        private async Task DeleteFriendFromFriendList()
        {
            var result = await _profileService.DeleteFromFriendList(UserId.Value);

            if (result)
            {
                IsUserFriend = false;
                NotificationService.Notify($"Successfully deleted {UserName} from friend list");
            }
            else
            {
                NotificationService.Notify("Something was wrong");
            }
        }

        private async Task GoToGamesVault()
        {
            await ShowViewModel<GamesVaultViewModel, ProfileUserModel>(new ProfileUserModel
            {
                UserId = _userId
            });
        }

        private async Task GoToWishList()
        {
            await ShowViewModel<WishListViewModel>();
        }

        private async Task GoToFriendsList()
        {
            await ShowViewModel<FriendsListViewModel>();
        }

        private async Task GoToSettings()
        {
            await ShowViewModel<SettingsViewModel>();
        }
    }
}
