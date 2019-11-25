using System.Collections.ObjectModel;
using System.Windows.Input;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Home
{
    public partial class HomeViewModel
    {
        private ObservableCollection<GameResponseModel> _comingSoonGames = new ObservableCollection<GameResponseModel>();

        public ObservableCollection<GameResponseModel> ComingSoonGames
        {
            get => _comingSoonGames;
            set => SetProperty(ref _comingSoonGames, value);
        }
        private ObservableCollection<GameResponseModel> _brandNewGames = new ObservableCollection<GameResponseModel>();

        public ObservableCollection<GameResponseModel> BrandNewGames
        {
            get => _brandNewGames;
            set => SetProperty(ref _brandNewGames, value);
        }
        private ObservableCollection<GameResponseModel> _hottestGames = new ObservableCollection<GameResponseModel>();

        public ObservableCollection<GameResponseModel> HottestGames
        {
            get => _hottestGames;
            set => SetProperty(ref _hottestGames, value);
        }
        private ObservableCollection<GameResponseModel> _onSaleGames = new ObservableCollection<GameResponseModel>();

        public ObservableCollection<GameResponseModel> OnSaleGames
        {
            get => _onSaleGames;
            set => SetProperty(ref _onSaleGames, value);
        }

        private ICommand _clickGame;
        public ICommand ClickGame => _clickGame ?? (_clickGame = new MvxAsyncCommand<GameResponseModel>(OpenGame));
    }
}
