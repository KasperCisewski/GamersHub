using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using GamersHub.Shared.Data.Enums;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Home
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IGameService _gameService;

        public HomeViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }

        public override async Task Initialize()
        {
            KeyboardService.HideKeyboard();
            Messenger.Publish(new ProgressBarActivator(this, true));
            var comingSoonGames = await _gameService.GetGames(HomeGamesCategory.ComingSoon);
            foreach (var game in comingSoonGames)
            {
                ComingSoonGames.Add(new GameWithImageRowModel(game));
            }

            var brandNewGames = await _gameService.GetGames(HomeGamesCategory.BrandNew);
            foreach (var game in brandNewGames)
            {
                BrandNewGames.Add(new GameWithImageRowModel(game));
            }

            var hottestGames = await _gameService.GetGames(HomeGamesCategory.Hottest);
            foreach (var game in hottestGames)
            {
                HottestGames.Add(new GameWithImageRowModel(game));
            }

            var onSaleGames = await _gameService.GetGames(HomeGamesCategory.OnSale);
            foreach (var game in onSaleGames)
            {
                OnSaleGames.Add(new GameWithImageRowModel(game));
            }
            Messenger.Publish(new ProgressBarActivator(this, false));
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }
    }
}