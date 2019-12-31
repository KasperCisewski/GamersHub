using GamerHub.mobile.core.Models;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameScreenshotsViewModel
    {
        private GameWithImageRowModel _gameModel;

        public GameWithImageRowModel GameModel
        {
            get => _gameModel;
            set => SetProperty(ref _gameModel, value);
        }

        private MvxObservableCollection<GameScreenshotRowModel> _gameScreenshots = new MvxObservableCollection<GameScreenshotRowModel>();

        public MvxObservableCollection<GameScreenshotRowModel> GameScreenshots
        {
            get => _gameScreenshots;
            set => SetProperty(ref _gameScreenshots, value);
        }
    }
}
