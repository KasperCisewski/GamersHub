using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;

namespace GamerHub.mobile.core.ViewModels.CoreApp.WishList
{
    public partial class WishListViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;

        public WishListViewModel(
            IProfileService profileService)
        {
            _profileService = profileService;
        }

        public override async Task Initialize()
        {
            var gamesInVault = await _profileService.GetWishListGames();

            foreach (var game in gamesInVault)
            {
                WishList.Add(new GameWithImageRowModel(game));
            }
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }
    }
}
