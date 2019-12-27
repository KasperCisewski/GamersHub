using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Resource;
using GamerHub.mobile.core.ViewModels.Base;
using GamersHub.Shared.Data.Enums;
using Enum = System.Enum;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Games
{
    public partial class GamesViewModel : BaseViewModel
    {
        private readonly IResourceService _resourceService;

        public GamesViewModel(
            IResourceService resourceService
            )
        {
            _resourceService = resourceService;
        }

        public override void Prepare()
        {
            foreach (var gameCategory in (GameCategory[])Enum.GetValues(typeof(GameCategory)))
            {
                CategoryList.Add(new GameCategoryModel(gameCategory, _resourceService));
            }
        }

        private async Task OpenCategoryGames(GameCategoryModel model)
        {
            await ShowViewModel<GamesCategoryViewModel, GameCategoryModel>(model);
        }

        private async Task OpenGamesChosenForUser()
        {
           await ShowViewModel<GamesChosenForUserViewModel>();
        }
    }
}
