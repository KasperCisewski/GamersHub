using System.Threading.Tasks;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GamePricesViewModel : BaseViewModel<GameWithImageRowModel>
    {
        private readonly IGameService _gameService;

        public GamePricesViewModel(
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
            var getPricesModelsForGame = await _gameService.GetPricesModelsForGame(GameModel.Id);
        }
    }
}
