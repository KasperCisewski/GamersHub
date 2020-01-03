using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.GamesVault
{
    public partial class GamesVaultHeatMapViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;

        public GamesVaultHeatMapViewModel(
            IProfileService profileService)
        {
            _profileService = profileService;
        }

        public override async Task Initialize()
        {
            var heatmapList = _profileService.GetHeatMap();
        }
    }
}
