using System.Threading.Tasks;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Dependency;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameViewModel : BaseViewModel<GameWithImageRowModel>
    {
        private readonly IGameService _gameService;
        public GameScreenshotsViewModel GameScreenshotsViewModel { get; }
        public GameVideoViewModel GameVideoViewModel { get; }
        public GamePricesViewModel GamePricesViewModel { get; }

        public GameViewModel(
            IDependencyService dependencyService,
            IGameService gameService)
        {
            _gameService = gameService;
            GameScreenshotsViewModel = dependencyService.Resolve<GameScreenshotsViewModel>();
            GameVideoViewModel = dependencyService.Resolve<GameVideoViewModel>();
            GamePricesViewModel = dependencyService.Resolve<GamePricesViewModel>();
        }
        public override void Prepare(GameWithImageRowModel parameter)
        {
            GameModel = parameter;
            GameScreenshotsViewModel.Prepare(parameter);
            GameVideoViewModel.Prepare(parameter);
            GamePricesViewModel.Prepare(parameter);
        }

        public override async Task Initialize()
        {
            var fullGameModel = await _gameService.GetFullGameModel(GameModel.Id);
            Description = fullGameModel.Description;
            ReleaseDate = fullGameModel.ReleaseDate;
            Title = fullGameModel.Title;
            GameCategoryText = GameModel.Category.ToString();
            GeneralImage = BitmapFactory.DecodeByteArray(fullGameModel.GeneralImage.ToArray(), 0, fullGameModel.GeneralImage.Count);
            await GameVideoViewModel.Initialize();
            await GameScreenshotsViewModel.Initialize();
            await GamePricesViewModel.Initialize();
        }

        private async Task AddGameToWishList()
        {
            var result = await _gameService.AddGameToWishList(GameModel.Id);

            NotificationService.Notify(result ? "Add game to wish list" : "Something was wrong");
        }

        private async Task AddGameToVault()
        {
            var result = await _gameService.AddGameToVault(GameModel.Id);

            NotificationService.Notify(result ? "Add game to vault" : "Something was wrong");
        }
    }
}
