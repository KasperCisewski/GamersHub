using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Db;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.Login;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Settings
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly ISqlLiteService _sqlLiteService;
        //private readonly IMvxPictureChooserTask _pictureChooserTask;

        public SettingsViewModel(
            ISqlLiteService sqlLiteService
           //  IMvxPictureChooserTask pictureChooserTask
        )
        {
            _sqlLiteService = sqlLiteService;
          //  _pictureChooserTask = pictureChooserTask;
        }

        private async Task Logout()
        {
            _sqlLiteService.ClearCredentials();
            await ShowViewModelAndRemoveHistory<LoginViewModel>();
        }

        private void DoTakePicture()
        {
            //_pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
        }

        private void DoChoosePicture()
        {
            //_pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
        }

        //private void OnPicture(Stream pictureStream)
        //{
        //    var memoryStream = new MemoryStream();
        //    pictureStream.CopyTo(memoryStream);
        //    Bytes = memoryStream.ToArray();
        //}
    }
}