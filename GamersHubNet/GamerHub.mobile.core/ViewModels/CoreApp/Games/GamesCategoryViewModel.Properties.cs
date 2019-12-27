using System.Windows.Input;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Games
{
    public partial class GamesCategoryViewModel
    {
        public GameCategoryModel GameCategoryModel { get; set; }

        private int _fetchedPages;

        private int FetchedPages
        {
            get => _fetchedPages;
            set => SetProperty(ref _fetchedPages, value);
        }

        private MvxObservableCollection<GameWithImageRowModel> _gamesList = new MvxObservableCollection<GameWithImageRowModel>();

        public MvxObservableCollection<GameWithImageRowModel> GamesList
        {
            get => _gamesList;
            set => SetProperty(ref _gamesList, value);
        }
        private ICommand _clickGame;
        public ICommand ClickGame => _clickGame ?? (_clickGame = new MvxAsyncCommand<GameWithImageRowModel>(OpenGame));
    }
}
