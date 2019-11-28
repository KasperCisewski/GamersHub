using GamerHub.mobile.core.Models;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GamePricesViewModel
    {
        private GameWithImageRowModel _gameModel;

        public GameWithImageRowModel GameModel
        {
            get => _gameModel;
            set => SetProperty(ref _gameModel, value);
        }
    }
}
