using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Friends
{
    public partial class FriendsListViewModel
    {
        private MvxObservableCollection<UserProfile> _friendsList = new MvxObservableCollection<UserProfile>();

        public MvxObservableCollection<UserProfile> FriendsList
        {
            get => _friendsList;
            set => SetProperty(ref _friendsList, value);
        }
        private IMvxAsyncCommand<UserProfile> _showFriendCommand;

        public IMvxAsyncCommand<UserProfile> ShowFriendCommand => 
            _showFriendCommand ?? (_showFriendCommand = new MvxAsyncCommand<UserProfile>(ShowFriend));

        private IMvxAsyncCommand _goToFriendSearchCommand;

        public IMvxAsyncCommand GoToFriendSearchCommand => 
            _goToFriendSearchCommand ?? (_goToFriendSearchCommand = new MvxAsyncCommand(GoToFriendSearch));
    }
}
