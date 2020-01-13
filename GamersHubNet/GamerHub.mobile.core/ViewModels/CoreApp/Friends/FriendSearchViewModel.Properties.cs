using System.Windows.Input;
using GamerHub.mobile.core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Friends
{
    public partial class FriendSearchViewModel
    {
        private string _searchUserNameText;

        public string SearchUserNameText
        {
            get => _searchUserNameText;
            set => SetProperty(ref _searchUserNameText, value);
        }

        private int _fetchedPages;

        private int FetchedPages
        {
            get => _fetchedPages;
            set => SetProperty(ref _fetchedPages, value);
        }

        private MvxObservableCollection<UserProfileModel> _friendsSearchList = new MvxObservableCollection<UserProfileModel>();

        public MvxObservableCollection<UserProfileModel> FriendsSearchList
        {
            get => _friendsSearchList;
            set => SetProperty(ref _friendsSearchList, value);
        }

        private ICommand _showFriendCommand;
        public ICommand ShowFriendCommand => _showFriendCommand ?? (_showFriendCommand = new MvxAsyncCommand<UserProfileModel>(ShowFriend));
    }
}
