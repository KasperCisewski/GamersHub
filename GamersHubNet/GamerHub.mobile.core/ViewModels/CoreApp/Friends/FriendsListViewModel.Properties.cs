using GamerHub.mobile.core.Models;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Friends
{
    public partial class FriendsListViewModel
    {
        private MvxObservableCollection<UserProfileModel> _friendsList = new MvxObservableCollection<UserProfileModel>();

        public MvxObservableCollection<UserProfileModel> FriendsList
        {
            get => _friendsList;
            set => SetProperty(ref _friendsList, value);
        }
        private IMvxAsyncCommand<UserProfileModel> _showFriendCommand;

        public IMvxAsyncCommand<UserProfileModel> ShowFriendCommand => 
            _showFriendCommand ?? (_showFriendCommand = new MvxAsyncCommand<UserProfileModel>(ShowFriend));

        private IMvxAsyncCommand _goToFriendSearchCommand;

        public IMvxAsyncCommand GoToFriendSearchCommand => 
            _goToFriendSearchCommand ?? (_goToFriendSearchCommand = new MvxAsyncCommand(GoToFriendSearch));
    }
}
