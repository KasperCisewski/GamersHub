using GamersHub.Shared.Contracts.Responses;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.WishList
{
    public partial class WishListViewModel
    {
        private MvxObservableCollection<GameModelWithImage> _wishList = new MvxObservableCollection<GameModelWithImage>();

        public MvxObservableCollection<GameModelWithImage> WishList
        {
            get => _wishList;
            set => SetProperty(ref _wishList, value);
        }
    }
}
