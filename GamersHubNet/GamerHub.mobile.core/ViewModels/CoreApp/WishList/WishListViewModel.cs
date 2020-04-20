using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using MvvmCross.ViewModels;

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

        public async Task FillGamesVault()
        {
            Messenger.Publish(new ProgressBarActivator(this, true));
            WishList = new MvxObservableCollection<GameWithImageRowModel>();

            var gamesInVault = await _profileService.GetWishListGames();

            foreach (var game in gamesInVault)
            {
                WishList.Add(new GameWithImageRowModel(game));
            }
            Messenger.Publish(new ProgressBarActivator(this, false));
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }
    }
}
