using System;
using Android.Graphics;
using MvvmCross.Commands;

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

        private bool _isOtherUserProfile;

        public bool IsOtherUserProfile
        {
            get => _isOtherUserProfile;
            set => SetProperty(ref _isOtherUserProfile, value);
        }

        private Bitmap _profileImageBitmap;

        public Bitmap ProfileImageBitmap
        {
            get => _profileImageBitmap;
            set => SetProperty(ref _profileImageBitmap, value);
        }

        private IMvxAsyncCommand _goToGamesVaultCommand;
        public IMvxAsyncCommand GoToGamesVaultCommand => _goToGamesVaultCommand ?? (_goToGamesVaultCommand = new MvxAsyncCommand(GoToGamesVault));

        private IMvxAsyncCommand _goToWishListCommand;
        public IMvxAsyncCommand GoToWishListCommand => _goToWishListCommand ?? (_goToWishListCommand = new MvxAsyncCommand(GoToWishList));

        private IMvxAsyncCommand _goToFriendsList;
        public IMvxAsyncCommand GoToFriendsListCommand => _goToFriendsList ?? (_goToFriendsList = new MvxAsyncCommand(GoToFriendsList));

        private IMvxAsyncCommand _goToSettings;
        public IMvxAsyncCommand GoToSettingsCommand => _goToSettings ?? (_goToSettings = new MvxAsyncCommand(GoToSettings));
    }
}
