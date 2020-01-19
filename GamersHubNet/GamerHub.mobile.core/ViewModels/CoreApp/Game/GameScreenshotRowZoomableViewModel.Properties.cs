using Android.Graphics;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameScreenshotRowZoomableViewModel
    {
        private Bitmap _screenShotBitmap;
        public Bitmap ScreenshotBitmap
        {
            get => _screenShotBitmap;
            set => SetProperty(ref _screenShotBitmap, value);
        }
    }
}

