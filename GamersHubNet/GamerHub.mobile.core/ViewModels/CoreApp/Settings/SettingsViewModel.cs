using System.Threading.Tasks;
using GamerHub.mobile.core.Services.Db;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.Login;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Settings
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly ISqlLiteService _sqlLiteService;

        public SettingsViewModel(
            ISqlLiteService sqlLiteService)
        {
            _sqlLiteService = sqlLiteService;
        }

        private async Task Logout()
        {
            _sqlLiteService.ClearCredentials();
            await ShowViewModelAndRemoveHistory<LoginViewModel>();
        }
    }
}
