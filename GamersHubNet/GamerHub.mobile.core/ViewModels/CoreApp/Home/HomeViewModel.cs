using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Home
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IGameService _gameService;

        public HomeViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }
        public override void Prepare()
        {
            base.Prepare();
        }

        private async Task OpenGame(GameModelWithImage arg)
        {
            throw new System.NotImplementedException();
        }
    }
}
