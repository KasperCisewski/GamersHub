using Android.Graphics;
using GamerHub.mobile.core.Services.Dependency;
using GamerHub.mobile.core.ViewModels.Base;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameViewModel : BaseViewModel<FullDescriptionGameModel>
    {
        private readonly IDependencyService _dependencyService;
        public GameScreenshotsViewModel GameScreenshotsViewModel { get; }
        public GameVideoViewModel GameVideoViewModel { get; }
        public GamePricesViewModel GamePricesViewModel { get; }

        public GameViewModel(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
            GameScreenshotsViewModel = _dependencyService.Resolve<GameScreenshotsViewModel>();
            GameVideoViewModel = _dependencyService.Resolve<GameVideoViewModel>();
            GamePricesViewModel = _dependencyService.Resolve<GamePricesViewModel>();
        }
        public override void Prepare(FullDescriptionGameModel parameter)
        {
            Bitmap = (Bitmap)Android.Graphics.Bitmap.FromArray(parameter.ImageTitle);
            GameScreenshotsViewModel.Prepare(parameter);
            GameVideoViewModel.Prepare(parameter);
            GamePricesViewModel.Prepare(parameter);
        }
    }
}
