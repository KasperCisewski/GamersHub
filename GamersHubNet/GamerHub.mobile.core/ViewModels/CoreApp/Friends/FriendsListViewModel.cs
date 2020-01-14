﻿using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.Services.Resource;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Profile;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Friends
{
    public partial class FriendsListViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;
        private readonly IResourceService _resourceService;

        public FriendsListViewModel(
            IProfileService profileService,
            IResourceService resourceService)
        {
            _profileService = profileService;
            _resourceService = resourceService;
        }

        public override async Task Initialize()
        {
            var userFriends = await _profileService.GetUserFriends();

            foreach (var friendModel in userFriends)
            {
                FriendsList.Add(new UserProfileModel(friendModel, _resourceService));
            }
        }

        private async Task GoToFriendSearch()
        {
            await ShowViewModel<FriendSearchViewModel>();
        }

        private async Task ShowFriend(UserProfileModel arg)
        {
            await ShowViewModel<ProfileViewModel, ProfileUserModel>(new ProfileUserModel
            {
                UserId = arg.Id
            });
        }
    }
}
