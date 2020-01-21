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
            UserHasGameInVault = fullGameModel.UserHasGameInVault;
            UserHasGameOnWishList = fullGameModel.UserHasGameOnWishList;
            GameCategoryText = GameModel.Category.ToString();
            GeneralImage = BitmapFactory.DecodeByteArray(fullGameModel.GeneralImage.ToArray(), 0, fullGameModel.GeneralImage.Count);
            await GameVideoViewModel.Initialize();
            await GameScreenshotsViewModel.Initialize();
            await GamePricesViewModel.Initialize();
        }

        private async Task AddGameToWishList()
        {
            var result = await _gameService.AddGameToWishList(GameModel.Id);

            if (result)
            {
                UserHasGameOnWishList = true;
                NotificationService.Notify("Added game to wish list");
            }
            else
            {
                NotificationService.Notify("Something was wrong");
            }
        }

        private async Task AddGameToVault()
        {
            var result = await _gameService.AddGameToVault(GameModel.Id);

            if (result)
            {
                UserHasGameInVault = true;
                NotificationService.Notify("Added game to vault");
            }
            else
            {
                NotificationService.Notify("Something was wrong");
            }
        }

        private async Task DeleteGameFromVault()
        {
            var result = await _gameService.DeleteGameFromVault(GameModel.Id);

            if (result)
            {
                UserHasGameInVault = false;
                NotificationService.Notify("Deleted game from vault");
            }
            else
            {
                NotificationService.Notify("Something was wrong");
            }
        }

        private async Task DeleteGameFromWishList()
        {
            var result = await _gameService.DeleteGameFromWishList(GameModel.Id);

            if (result)
            {
                UserHasGameOnWishList = false;
                NotificationService.Notify("Deleted game from wish list");
            }
            else
            {
                NotificationService.Notify("Something was wrong");
            }
        }
    }
}
