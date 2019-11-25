using System.Collections.ObjectModel;
using System.Windows.Input;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Home
{
    public partial class HomeViewModel
    {
        private ObservableCollection<GameModelWithImage> _comingSoonGames = new ObservableCollection<GameModelWithImage>();

        public ObservableCollection<GameModelWithImage> ComingSoonGames
        {
            get => _comingSoonGames;
            set => SetProperty(ref _comingSoonGames, value);
        }
        private ObservableCollection<GameModelWithImage> _brandNewGames = new ObservableCollection<GameModelWithImage>();

        public ObservableCollection<GameModelWithImage> BrandNewGames
        {
            get => _brandNewGames;
            set => SetProperty(ref _brandNewGames, value);
        }
        private ObservableCollection<GameModelWithImage> _hottestGames = new ObservableCollection<GameModelWithImage>();

        public ObservableCollection<GameModelWithImage> HottestGames
        {
            get => _hottestGames;
            set => SetProperty(ref _hottestGames, value);
        }
        private ObservableCollection<GameModelWithImage> _onSaleGames = new ObservableCollection<GameModelWithImage>();

        public ObservableCollection<GameModelWithImage> OnSaleGames
        {
            get => _onSaleGames;
            set => SetProperty(ref _onSaleGames, value);
        }

        private ICommand _clickGame;
        public ICommand ClickGame => _clickGame ?? (_clickGame = new MvxAsyncCommand<GameModelWithImage>(OpenGame));
    }
}
