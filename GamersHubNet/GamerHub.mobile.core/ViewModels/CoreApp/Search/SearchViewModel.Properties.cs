using System.Collections.ObjectModel;
using GamersHub.Shared.Contracts.Responses;

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

        private ObservableCollection<GameModelWithImage> _gamesSearchList = new ObservableCollection<GameModelWithImage>();

        public ObservableCollection<GameModelWithImage> GamesSearchList
        {
            get => _gamesSearchList;
            set => SetProperty(ref _gamesSearchList, value);
        }
    }
}
