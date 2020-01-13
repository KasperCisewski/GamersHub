using GamerHub.mobile.core.Models;
using MvvmCross.ViewModels;

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

        private MvxObservableCollection<GameOfferRowModel> _gameOffers = new MvxObservableCollection<GameOfferRowModel>();

        public MvxObservableCollection<GameOfferRowModel> GameOffers
        {
            get => _gameOffers;
            set => SetProperty(ref _gameOffers, value);
        }
    }
}
