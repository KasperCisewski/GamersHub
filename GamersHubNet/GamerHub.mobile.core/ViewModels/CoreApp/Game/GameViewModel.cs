using System.Threading.Tasks;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameViewModel : BaseViewModel<GameWithImageRowModel>
    {
        private readonly IGameService _gameService;
        private MvxSubscriptionToken _openVideoCardToken;

        public GameViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }
        public override void Prepare(GameWithImageRowModel parameter)
        {
            GameModel = parameter;
        }

        public override async Task Initialize()
        {
            Messenger.Publish(new ProgressBarActivator(this, true));
            var fullGameModel = await _gameService.GetFullGameModel(GameModel.Id);
            Description = fullGameModel.Description;
            ReleaseDate = fullGameModel.ReleaseDate;
            Title = fullGameModel.Title;
            UserHasGameInVault = fullGameModel.UserHasGameInVault;
            UserHasGameOnWishList = fullGameModel.UserHasGameOnWishList;
            GameCategoryText = GameModel.Category.ToString();
            GeneralImage = BitmapFactory.DecodeByteArray(fullGameModel.GeneralImage.ToArray(), 0, fullGameModel.GeneralImage.Count);
            VideoUrl = await _gameService.GetVideoUrlForGame(GameModel.Id);
            Messenger.Publish(new OpenVideoCardView(this));
            var screenshotList = await _gameService.GetScreenShotsForGame(GameModel.Id);

            foreach (var screenshot in screenshotList)
            {
                GameScreenshots.Add(new GameScreenshotRowModel(screenshot.ImageContent));
            }

            ShouldShowGameScreenshots = true;
            ShouldShowGameVideo = false;
            Messenger.Publish(new ProgressBarActivator(this, false));
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

        private async Task OpenGameScreenshot(GameScreenshotRowModel parameter)
        {
            await ShowViewModel<GameScreenshotRowZoomableViewModel, GameScreenshotRowModel>(parameter);
        }

        private void ShowGameScreenshotsTab()
        {
            ShouldShowGameVideo = false;
            ShouldShowGameScreenshots = true;
        }

        private void ShowGameVideoTab()
        {
            ShouldShowGameVideo = true;
            ShouldShowGameScreenshots = false;
        }
    }
}
