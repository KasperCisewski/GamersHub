using System.Windows.Input;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.WishList
{
    public partial class WishListViewModel
    {
        private MvxObservableCollection<GameWithImageRowModel> _wishList = new MvxObservableCollection<GameWithImageRowModel>();

        public MvxObservableCollection<GameWithImageRowModel> WishList
        {
            get => _wishList;
            set => SetProperty(ref _wishList, value);
        }
        private ICommand _clickGame;
        public ICommand ClickGame => _clickGame ?? (_clickGame = new MvxAsyncCommand<GameWithImageRowModel>(OpenGame));
    }
}
