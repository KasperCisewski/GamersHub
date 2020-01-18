using System;
using System.Windows.Input;
using Android.Graphics;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GameViewModel
    {
        private GameWithImageRowModel _gameModel;

        public GameWithImageRowModel GameModel
        {
            get => _gameModel;
            set => SetProperty(ref _gameModel, value);
        }

        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private DateTime? _releaseDate;

        public DateTime? ReleaseDate
        {
            get => _releaseDate;
            set => SetProperty(ref _releaseDate, value);
        }

        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _gameCategoryText;

        public string GameCategoryText
        {
            get => _gameCategoryText;
            set => SetProperty(ref _gameCategoryText, value);
        }

        private Bitmap _generalImage;

        public Bitmap GeneralImage
        {
            get => _generalImage;
            set => SetProperty(ref _generalImage, value);
        }

        private bool _isUserHasGameInVault;

        public bool IsUserHasGameInVault
        {
            get => _isUserHasGameInVault;
            set => SetProperty(ref _isUserHasGameInVault, value);
        }

        private bool _isUserHasGameInWishList;

        public bool IsUserHasGameInWishList
        {
            get => _isUserHasGameInWishList;
            set => SetProperty(ref _isUserHasGameInWishList, value);
        }

        private ICommand _addGameToWishListCommand;

        public ICommand AddGameToWishListCommand => _addGameToWishListCommand ?? (_addGameToWishListCommand = new MvxAsyncCommand(AddGameToWishList));

        private ICommand _addGameToVaultCommand;

        public ICommand AddGameToVaultCommand => _addGameToVaultCommand ?? (_addGameToVaultCommand = new MvxAsyncCommand(AddGameToVault));

        private ICommand _deleteGameFromWishListCommand;

        public ICommand DeleteGameFromWishListCommand => _deleteGameFromWishListCommand ?? (_deleteGameFromWishListCommand = new MvxAsyncCommand(DeleteGameFromWishList));

        private ICommand _deleteGameFromVaultCommand;

        public ICommand DeleteGameFromVaultCommand => _deleteGameFromVaultCommand ?? (_deleteGameFromVaultCommand = new MvxAsyncCommand(DeleteGameFromVault));
    }
}
