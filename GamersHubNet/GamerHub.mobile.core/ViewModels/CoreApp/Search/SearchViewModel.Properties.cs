using System.Collections.ObjectModel;
using GamerHub.mobile.core.Models;

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

        private ObservableCollection<GameWithImageRowModel> _gamesSearchList = new ObservableCollection<GameWithImageRowModel>();

        public ObservableCollection<GameWithImageRowModel> GamesSearchList
        {
            get => _gamesSearchList;
            set => SetProperty(ref _gamesSearchList, value);
        }
    }
}
