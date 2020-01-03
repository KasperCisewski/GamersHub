﻿using System.Threading.Tasks;
using GamerHub.mobile.core.Infrastructure;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Profile;
using GamersHub.Shared.Contracts.Requests;
using MvvmCross.ViewModels;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Friends
{
    public partial class FriendSearchViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;

        public FriendSearchViewModel(
            IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task SearchFriends(bool replace)
        {
            if (replace)
            {
                FetchedPages = 0;
                FriendsSearchList = new MvxObservableCollection<UserProfileModel>();
            }

            var listRowModels = await _profileService.SearchUsers(new SearchFriendsRequest
            {
                Skip = FetchedPages * StaticAppSettings.PullDataPageSize,
                Take = StaticAppSettings.PullDataPageSize,
                SearchUserNameText = SearchUserNameText
            });

            if (listRowModels != null)
            {
                foreach (var rowModel in listRowModels)
                {
                    FriendsSearchList.Add(new UserProfileModel(rowModel));
                }

                FetchedPages++;
            }
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
