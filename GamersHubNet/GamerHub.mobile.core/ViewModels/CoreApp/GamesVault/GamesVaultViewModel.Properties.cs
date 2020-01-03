using System;
using System.Windows.Input;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;
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

        private MvxObservableCollection<GameWithImageRowModel> _gamesList = new MvxObservableCollection<GameWithImageRowModel>();

        public MvxObservableCollection<GameWithImageRowModel> GamesList
        {
            get => _gamesList;
            set => SetProperty(ref _gamesList, value);
        }

        private ICommand _clickGame;
        public ICommand ClickGame => _clickGame ?? (_clickGame = new MvxAsyncCommand<GameWithImageRowModel>(OpenGame));

        private ICommand _goToHeatMapCommand;
        public ICommand GoToHeatMapCommand => _goToHeatMapCommand ?? (_goToHeatMapCommand = new MvxAsyncCommand(OpenHeatMap));
    }
}
