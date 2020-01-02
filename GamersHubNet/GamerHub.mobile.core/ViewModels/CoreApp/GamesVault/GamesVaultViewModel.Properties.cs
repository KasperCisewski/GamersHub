using System;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.GamesVault
{
    public partial class GamesVaultViewModel
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

        private MvxObservableCollection<GameModelWithImage> _gamesList = new MvxObservableCollection<GameModelWithImage>();

        public MvxObservableCollection<GameModelWithImage> GamesList
        {
            get => _gamesList;
            set => SetProperty(ref _gamesList, value);
        }
    }
}
