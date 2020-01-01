using System;
using Android.Graphics;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Profile
{
    public partial class ProfileViewModel
    {
        private string _userName;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private Guid? _userId;

        public Guid? UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private bool _isUserProfile;

        public bool IsUserProfile
        {
            get => _isUserProfile;
            set => SetProperty(ref _isUserProfile, value);
        }

        private Bitmap _profileImageBitmap;

        public Bitmap ProfileImageBitmap
        {
            get => _profileImageBitmap;
            set => SetProperty(ref _profileImageBitmap, value);
        }
    }
}
