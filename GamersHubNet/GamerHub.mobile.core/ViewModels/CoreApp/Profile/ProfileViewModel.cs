using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;

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
            IsUserProfile = parameter.UserId != Guid.Empty;
            UserId = IsUserProfile ? parameter.UserId : (Guid?)null;
        }

        public override async Task Initialize()
        {
            var model = await _profileService.GetUserProfileInformation(_userId);
            UserName = model.UserName;
            ProfileImageBitmap = BitmapFactory.DecodeByteArray(model.ProfileImageContent.ToArray(), 0, model.ProfileImageContent.Count);
        }
    }
}
