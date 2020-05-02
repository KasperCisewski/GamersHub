using System.Threading.Tasks;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.GamesVault
{
    public partial class GamesVaultHeatMapViewModel : BaseViewModel<ProfileUserModel>
    {
        private readonly IProfileService _profileService;

        public GamesVaultHeatMapViewModel(
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
            Messenger.Publish(new ProgressBarActivator(this, true));
            var heatmapList = await _profileService.GetHeatMap(_userId);

            HeatMapBitmap = BitmapFactory.DecodeByteArray(heatmapList.ToArray(), 0, heatmapList.Count);
            Messenger.Publish(new ProgressBarActivator(this, false));
        }
    }
}
