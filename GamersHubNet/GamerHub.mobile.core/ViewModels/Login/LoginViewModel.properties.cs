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
    }
}
