using MvvmCross.Commands;
using System.Windows.Input;

namespace GamerHub.mobile.core.ViewModels.Registration
{
    public partial class RegistrationViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nameErrorMessage;
        public string NameErrorMessage
        {
            get => _nameErrorMessage;
            set => SetProperty(ref _nameErrorMessage, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _emailErrorMessage;
        public string EmailErrorMessage
        {
            get => _emailErrorMessage;
            set => SetProperty(ref _emailErrorMessage, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _passwordErrorMessage;
        public string PasswordErrorMessage
        {
            get => _passwordErrorMessage;
            set => SetProperty(ref _passwordErrorMessage, value);
        }

        private string _repeatablePassword;
        public string RepeatablePassword
        {
            get => _repeatablePassword;
            set => SetProperty(ref _repeatablePassword, value);
        }

        private string _repeatablePasswordErrorMessage;
        public string RepeatablePasswordErrorMessage
        {
            get => _repeatablePasswordErrorMessage;
            set => SetProperty(ref _repeatablePasswordErrorMessage, value);
        }

        private bool _isValidForm;
        public bool IsValidForm
        {
            get => _isValidForm;
            set => SetProperty(ref _isValidForm, value);
        }

        private bool _isValidName;
        public bool IsValidName
        {
            get => _isValidName;
            set => SetProperty(ref _isValidName, value);
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

        private bool _isValidRepeatablePassword;
        public bool IsValidRepeatablePassword
        {
            get => _isValidRepeatablePassword;
            set => SetProperty(ref _isValidRepeatablePassword, value);
        }

        public ICommand SubmitRegistrationFormCommand => new MvxAsyncCommand(SubmitRegistrationForm);
    }
}
