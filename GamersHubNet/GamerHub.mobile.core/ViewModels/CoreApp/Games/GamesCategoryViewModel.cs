using System.Threading.Tasks;
using GamerHub.mobile.core.Infrastructure;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;
using GamersHub.Shared.Contracts.Requests;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Games
{
    public partial class GamesCategoryViewModel : BaseViewModel<GameCategoryModel>
    {
        private readonly IGameService _gameService;

        public GamesCategoryViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }

        public override void Prepare(GameCategoryModel parameter)
        {
            GameCategoryModel = parameter;
        }

        public async Task SearchGames(bool replace)
        {
            if (replace)
            {
                FetchedPages = 0;
                GamesList = new MvxObservableCollection<GameWithImageRowModel>();
            }

            var listRowModels = await _gameService.GetGamesByCategory(new GameCategoryRequest
            {
                Skip = FetchedPages * StaticAppSettings.PullDataPageSize,
                Take = StaticAppSettings.PullDataPageSize,
                GameCategory = GameCategoryModel.GameCategory
            });

            if (listRowModels != null)
            {
                foreach (var rowModel in listRowModels)
                {
                    GamesList.Add(new GameWithImageRowModel(rowModel));
                }

                FetchedPages++;
            }
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }
    }
}
