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

        private DateTime _releaseDate;

        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set => SetProperty(ref _releaseDate, value);
        }

        private Bitmap _generalImage;

        public Bitmap GeneralImage
        {
            get => _generalImage;
            set => SetProperty(ref _generalImage, value);
        }

        private ICommand _addGameToWishListCommand;

        public ICommand AddGameToWishListCommand => _addGameToWishListCommand ?? (_addGameToWishListCommand = new MvxAsyncCommand(AddGameToWishList));

        private ICommand _addGameToVaultCommand;

        public ICommand AddGameToVaultCommand => _addGameToVaultCommand ?? (_addGameToVaultCommand = new MvxAsyncCommand(AddGameToVault));
    }
}
