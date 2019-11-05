using MvvmCross.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GamerHub.mobile.core.ViewModels.Login
{
    public partial class LoginViewModel
    {
        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public bool _saveCredentials;
        public bool SaveCredentials
        {
            get => _saveCredentials;
            set => SetProperty(ref _saveCredentials, value);
        }

        public ICommand LogInCommand => new MvxAsyncCommand(TryToLogIntoApp);

        private ICommand GoToRegistrationViewCommand => new MvxAsyncCommand(GoToRegistrationView);
    }
}
