using MvvmCross.Commands;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Settings
{
    public partial class SettingsViewModel
    {
        private IMvxAsyncCommand _logoutCommand;

        public IMvxAsyncCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = new MvxAsyncCommand(Logout));
    }
}
