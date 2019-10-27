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
    }
}
