using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameVideoViewModel : BaseViewModel<GameWithImageRowModel>
    {
        private readonly IGameService _gameService;
        private MvxSubscriptionToken _openVideoCardToken;
        public GameVideoViewModel(
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
            VideoUrl = await _gameService.GetVideoUrlForGame(GameModel.Id);
            await base.Initialize();
        }

        public void GetYoutubeVideo()
        {
            Messenger.Publish(new OpenVideoCardView(this));
        }
    }
}
