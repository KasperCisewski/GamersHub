using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameScreenshotRowZoomableViewModel : BaseViewModel<GameScreenshotRowModel>
    {
        public override void Prepare(GameScreenshotRowModel parameter)
        {
            ScreenshotBitmap = parameter.ScreenshotBitmap;
        }
    }
}
