namespace GamerHub.mobile.core.ViewModels.CoreApp.Profile
{
    public partial class ProfileViewModel
    {
        private string _userLogin;

        public string UserLogin
        {
            get => _userLogin;
            set => SetProperty(ref _userLogin, value);
        }
    }
}
