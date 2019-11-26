using Android.Graphics;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameViewModel
    {
        private Bitmap _bitMap;

        public Bitmap Bitmap
        {
            get => _bitMap;
            set => SetProperty(ref _bitMap, value);
        }
    }
}
