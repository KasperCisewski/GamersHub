using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Dependency;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameViewModel : BaseViewModel<GameWithImageRowModel>
    {
        private readonly IDependencyService _dependencyService;
        private readonly IGameService _gameService;
        public GameScreenshotsViewModel GameScreenshotsViewModel { get; }
        public GameVideoViewModel GameVideoViewModel { get; }
        public GamePricesViewModel GamePricesViewModel { get; }

        public GameViewModel(
            IDependencyService dependencyService,
            IGameService gameService)
        {
            _dependencyService = dependencyService;
            _gameService = gameService;
            GameScreenshotsViewModel = _dependencyService.Resolve<GameScreenshotsViewModel>();
            GameVideoViewModel = _dependencyService.Resolve<GameVideoViewModel>();
            GamePricesViewModel = _dependencyService.Resolve<GamePricesViewModel>();
        }
        public override void Prepare(GameWithImageRowModel parameter)
        {
            GameScreenshotsViewModel.Prepare(parameter);
            GameVideoViewModel.Prepare(parameter);
            GamePricesViewModel.Prepare(parameter);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }
    }
}
