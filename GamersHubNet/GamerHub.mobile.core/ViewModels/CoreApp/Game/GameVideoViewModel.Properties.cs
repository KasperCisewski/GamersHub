using GamerHub.mobile.core.Models;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameVideoViewModel
    {
        private GameWithImageRowModel _gameModel;

        public GameWithImageRowModel GameModel
        {
            get => _gameModel;
            set => SetProperty(ref _gameModel, value);
        }

        private string _videoUrl;

        public string VideoUrl
        {
            get => _videoUrl;
            set => SetProperty(ref _videoUrl, value);
        }
    }
}
