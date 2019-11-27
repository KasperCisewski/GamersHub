using System.Collections.ObjectModel;
using System.Windows.Input;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Search
{
    public partial class SearchViewModel
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _fetchedPages;

        private int FetchedPages
        {
            get => _fetchedPages;
            set => SetProperty(ref _fetchedPages, value);
        }

        private ObservableCollection<GameWithImageRowModel> _gamesSearchList = new ObservableCollection<GameWithImageRowModel>();

        public ObservableCollection<GameWithImageRowModel> GamesSearchList
        {
            get => _gamesSearchList;
            set => SetProperty(ref _gamesSearchList, value);
        }
        private ICommand _clickGame;
        public ICommand ClickGame => _clickGame ?? (_clickGame = new MvxAsyncCommand<GameWithImageRowModel>(OpenGame));
    }
}
