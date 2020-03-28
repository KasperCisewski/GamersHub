using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
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

            var comingSoonGames = await _gameService.GetGames(HomeGamesCategory.ComingSoon);
            foreach (var game in comingSoonGames)
            {
                ComingSoonGames.Add(new GameWithImageRowModel(game));
            }

            var comingSoonGamesModels = ComingSoonGames.ToList();
            ComingSoonGames = new ObservableCollection<GameWithImageRowModel>();

            var highestWidth = comingSoonGamesModels.OrderByDescending(c => c.ImageBitmap.Width).FirstOrDefault()
                .ImageBitmap.Width;
            var highestHeight = comingSoonGamesModels.OrderByDescending(c => c.ImageBitmap.Height).FirstOrDefault()
                .ImageBitmap.Height;

            foreach (var comingSoonGamesModel in comingSoonGamesModels)
            {
                ComingSoonGames.Add(new GameWithImageRowModel(comingSoonGamesModel, highestWidth, highestHeight));
            }

            var brandNewGames = await _gameService.GetGames(HomeGamesCategory.BrandNew);
            foreach (var game in brandNewGames)
            {
                var model = new GameWithImageRowModel(game);
                BrandNewGames.Add(new GameWithImageRowModel(model, highestWidth, highestHeight));
            }

            var hottestGames = await _gameService.GetGames(HomeGamesCategory.Hottest);
            foreach (var game in hottestGames)
            {
                var model = new GameWithImageRowModel(game);
                HottestGames.Add(new GameWithImageRowModel(model, highestWidth, highestHeight));
            }

            var onSaleGames = await _gameService.GetGames(HomeGamesCategory.OnSale);
            foreach (var game in onSaleGames)
            {
                var model = new GameWithImageRowModel(game);
                OnSaleGames.Add(new GameWithImageRowModel(model, highestWidth, highestHeight));
            }

            //var wholeGames = comingSoonGames
            //    .Concat(brandNewGames)
            //    .Concat(hottestGames)
            //    .Concat(onSaleGames);
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }
    }
}
