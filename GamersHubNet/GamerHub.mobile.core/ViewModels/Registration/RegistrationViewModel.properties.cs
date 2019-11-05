namespace GamerHub.mobile.core.ViewModels.Registration
{
    public partial class RegistrationViewModel
    {
        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _repetablePassword;
        public string RepetablePassword
        {
            get => _repetablePassword;
            set => SetProperty(ref _repetablePassword, value);
        }

        private bool _isValidForm;
        public bool IsValidForm
        {
            get => _isValidForm;
            set => SetProperty(ref _isValidForm, value);
        }

        private bool _isValidLogin;
        public bool IsValidLogin
        {
            get => _isValidLogin;
            set => SetProperty(ref _isValidLogin, value);
        }

        private bool _isValidEmail;
        public bool IsValidEmail
        {
            get => _isValidEmail;
            set => SetProperty(ref _isValidEmail, value);
        }

        private bool _isValidPassword;
        public bool IsValidPassword
        {
            get => _isValidPassword;
            set => SetProperty(ref _isValidPassword, value);
        }

        private bool _isValidRepetablePassword;
        public bool IsValidRepetablePassword
        {
            get => _isValidRepetablePassword;
            set => SetProperty(ref _isValidRepetablePassword, value);
        }
    }
}
