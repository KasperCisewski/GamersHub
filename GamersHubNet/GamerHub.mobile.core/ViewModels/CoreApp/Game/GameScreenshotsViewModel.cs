using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameScreenshotsViewModel : BaseViewModel<GameWithImageRowModel>
    {
        private readonly IGameService _gameService;

        public GameScreenshotsViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }

        public override void Prepare(GameWithImageRowModel parameter)
        {
            GameModel = parameter;
        }

        public override async Task Initialize()
        {
            var screenShotList = await _gameService.GetScreenShotsForGame(GameModel.Id);
        }
    }
}
