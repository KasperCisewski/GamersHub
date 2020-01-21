using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.GamesVault
{
    public partial class GamesVaultViewModel : BaseViewModel<ProfileUserModel>
    {
        private readonly IProfileService _profileService;

        public GamesVaultViewModel(
            IProfileService profileService)
        {
            _profileService = profileService;
        }

        public override void Prepare(ProfileUserModel parameter)
        {
            IsOtherUserProfile = parameter.UserId != null;
            UserId = IsOtherUserProfile ? parameter.UserId : null;
        }

        public async Task FillGamesVault()
        {
            GamesList = new MvxObservableCollection<GameWithImageRowModel>();

            var gamesInVault = await _profileService.GetGamesInVault(_userId);

            foreach (var game in gamesInVault)
            {
                GamesList.Add(new GameWithImageRowModel(game));
            }
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }

        private async Task OpenHeatMap()
        {
            await ShowViewModel<GamesVaultHeatMapViewModel, ProfileUserModel>(new ProfileUserModel
            {
                UserId = UserId
            });
        }
    }
}
