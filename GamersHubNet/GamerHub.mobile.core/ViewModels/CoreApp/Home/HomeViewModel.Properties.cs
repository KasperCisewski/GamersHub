using System.Collections.ObjectModel;
using System.Windows.Input;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Home
{
    public partial class HomeViewModel
    {
        private ObservableCollection<GameWithImageRowModel> _comingSoonGames = new ObservableCollection<GameWithImageRowModel>();

        public ObservableCollection<GameWithImageRowModel> ComingSoonGames
        {
            get => _comingSoonGames;
            set => SetProperty(ref _comingSoonGames, value);
        }
        private ObservableCollection<GameWithImageRowModel> _brandNewGames = new ObservableCollection<GameWithImageRowModel>();

        public ObservableCollection<GameWithImageRowModel> BrandNewGames
        {
            get => _brandNewGames;
            set => SetProperty(ref _brandNewGames, value);
        }
        private ObservableCollection<GameWithImageRowModel> _hottestGames = new ObservableCollection<GameWithImageRowModel>();

        public ObservableCollection<GameWithImageRowModel> HottestGames
        {
            get => _hottestGames;
            set => SetProperty(ref _hottestGames, value);
        }
        private ObservableCollection<GameWithImageRowModel> _onSaleGames = new ObservableCollection<GameWithImageRowModel>();

        public ObservableCollection<GameWithImageRowModel> OnSaleGames
        {
            get => _onSaleGames;
            set => SetProperty(ref _onSaleGames, value);
        }

        private ICommand _clickGame;
        public ICommand ClickGame => _clickGame ?? (_clickGame = new MvxAsyncCommand<GameWithImageRowModel>(OpenGame));
    }
}
