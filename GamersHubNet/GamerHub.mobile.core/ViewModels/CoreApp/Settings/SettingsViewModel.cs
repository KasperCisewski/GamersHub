using System.Threading.Tasks;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.Login;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Settings
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private async Task Logout()
        {
            await ShowViewModelAndRemoveHistory<LoginViewModel>();
        }
    }
}
