using Android.Graphics;

namespace GamerHub.mobile.core.ViewModels.CoreApp.GamesVault
{
    public partial class GamesVaultHeatMapViewModel
    {
        private Bitmap _heatMapBitmap;

        public Bitmap HeatMapBitmap
        {
            get => _heatMapBitmap;
            set => SetProperty(ref _heatMapBitmap, value);
        }
    }
}
