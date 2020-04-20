using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Game;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Games
{
    public partial class GamesChosenForUserViewModel : BaseViewModel
    {
        private readonly IGameService _gameService;

        public GamesChosenForUserViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }

        public override async Task Initialize()
        {
            Messenger.Publish(new ProgressBarActivator(this, true));
            var gamesForUser = await _gameService.GetGamesForUser();

            foreach (var game in gamesForUser)
            {
                GamesList.Add(new GameWithImageRowModel(game));
            }
            Messenger.Publish(new ProgressBarActivator(this, false));
        }

        private async Task OpenGame(GameWithImageRowModel arg)
        {
            await ShowViewModel<GameViewModel, GameWithImageRowModel>(arg);
        }
    }
}
