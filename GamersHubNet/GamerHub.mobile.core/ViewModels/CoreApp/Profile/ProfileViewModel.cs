using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.Content.Res;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
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

        public ProfileViewModel(
            IProfileService profileService)
        {
            _profileService = profileService;
        }

        public override void Prepare(ProfileUserModel parameter)
        {
            IsOtherUserProfile = parameter.UserId != null;
            UserId = IsOtherUserProfile ? parameter.UserId : null;
        }

        public override async Task Initialize()
        {
            var model = await _profileService.GetUserProfileInformation(_userId);
            UserName = model.UserName;
            //TODO
            if (model.ProfileImageContent != null && model.ProfileImageContent.Any())
            {
                ProfileImageBitmap = BitmapFactory.DecodeByteArray(model.ProfileImageContent.ToArray(), 0, model.ProfileImageContent.Count);
            }
            else
            {
                var profileImageId = (int)typeof(Resource.Drawable).GetField("ProfileImage.png").GetValue(null);
                ProfileImageBitmap = BitmapFactory.DecodeResource(Resources.System, profileImageId);
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
