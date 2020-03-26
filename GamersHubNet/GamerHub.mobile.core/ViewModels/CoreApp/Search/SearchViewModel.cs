using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GamerHub.mobile.core.Infrastructure;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Search;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using GamersHub.Shared.Contracts.Requests;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Search
{
    public partial class SearchViewModel : BaseViewModel
    {
        private readonly ISearchService _searchService;

        public SearchViewModel(
            ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task SearchGames(bool replace)
        {
            if (replace)
            {
                FetchedPages = 0;
                GamesSearchList = new ObservableCollection<GameWithImageRowModel>();
            }

            var listRowModels = await _searchService.GetSearchGamesModels(new SearchGameRequest
            {
                Skip = FetchedPages * StaticAppSettings.PullDataPageSize,
                Take = StaticAppSettings.PullDataPageSize,
                SearchGameText = SearchText
            });

            if (listRowModels != null)
            {
                foreach (var rowModel in listRowModels)
                {
                    GamesSearchList.Add(new GameWithImageRowModel(rowModel));
                }

                FetchedPages++;
            }
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }
    }
}
