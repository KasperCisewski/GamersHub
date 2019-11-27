using System;
using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;

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

        public override Task Initialize()
        {
            return base.Initialize();
        }


        private async Task OpenGame(GameWithImageRowModel arg)
        {
            throw new NotImplementedException();
        }
    }
}
