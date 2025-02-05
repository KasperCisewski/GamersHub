﻿using System.IO;
using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Db;
using GamerHub.mobile.core.Services.Profile;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.Login;
using MvvmCross.Plugin.PictureChooser;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Settings
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly ISqlLiteService _sqlLiteService;
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IProfileService _profileService;

        public SettingsViewModel(
            ISqlLiteService sqlLiteService,
            IMvxPictureChooserTask pictureChooserTask,
            IProfileService profileService)
        {
            _sqlLiteService = sqlLiteService;
            _pictureChooserTask = pictureChooserTask;
            _profileService = profileService;
            _profileService = profileService;
        }

        private async Task Logout()
        {
            _sqlLiteService.ClearCredentials();
            await ShowViewModelAndRemoveHistory<LoginViewModel>();
        }

        private void DoTakePicture()
        {
            _pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
        }

        private void DoChoosePicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
        }

        private async void OnPicture(Stream pictureStream)
        {
            var memoryStream = new MemoryStream();
            pictureStream.CopyTo(memoryStream);
            Bytes = memoryStream.ToArray();

            await Task.Run(async () =>
            {
                var result = await _profileService.EditProfileImage(Bytes);

                NotificationService.Notify(result ? $"Successfully changed profile view" : "Something was wrong");
            });
        }
    }
}