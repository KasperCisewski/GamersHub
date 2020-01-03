using System;
using Android.Graphics;

namespace GamerHub.mobile.core.ViewModels.CoreApp.GamesVault
{
    public partial class GamesVaultHeatMapViewModel
    {
        private Guid? _userId;

        public Guid? UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private bool _isOtherUserProfile;

        public bool IsOtherUserProfile
        {
            get => _isOtherUserProfile;
            set => SetProperty(ref _isOtherUserProfile, value);
        }

        private Bitmap _heatMapBitmap;

        public Bitmap HeatMapBitmap
        {
            get => _heatMapBitmap;
            set => SetProperty(ref _heatMapBitmap, value);
        }
    }
}
