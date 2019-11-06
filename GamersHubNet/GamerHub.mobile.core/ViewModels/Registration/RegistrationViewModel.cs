using GamerHub.mobile.core.Models.LoginAndRegistration;
using GamerHub.mobile.core.Services;
using GamerHub.mobile.core.Services.Account;
using GamerHub.mobile.core.ViewModels.Base;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.ViewModels.Registration
{
    public partial class RegistrationViewModel : BaseViewModel<LoginModel>
    {
        private readonly IAccountService _accountService;
        private readonly ILocalizationService _localizationService;

        public RegistrationViewModel(
            IAccountService accountService,
            ILocalizationService localizationService)
        {
            _accountService = accountService;
            _localizationService = localizationService;
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsValidForm = IsValidEmail && IsValidName && IsValidPassword && IsValidRepeatablePassword;
        }

        public override void Prepare(LoginModel parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter.UserEmailOrName))
            {
                if (parameter.UserEmailOrName.Contains("@"))
                {
                    Email = parameter.UserEmailOrName;
                }
                else
                {
                    Name = parameter.UserEmailOrName;
                }
            }
            IsValidEmail = true;
            IsValidName = true;
            IsValidPassword = true;
            IsValidRepeatablePassword = true;
        }

        public async Task ValidName()
        {
            if (!string.IsNullOrWhiteSpace(Name) && Name.Length < 3)
            {
                NameErrorMessage = _localizationService.GetString("name_is_too_short"); ;
                IsValidName = false;
                return;
            }

            var result = await _accountService.CheckIfNameExist(Name);
            if (result)
            {
                NameErrorMessage = _localizationService.GetString("name_exist_in_system");
                IsValidName = false;
                return;
            }

            IsValidName = true;
        }

        public async Task ValidEmail()
        {
            if (!string.IsNullOrWhiteSpace(Email) && Email.Length < 3)
            {
                EmailErrorMessage = _localizationService.GetString("email_is_too_short"); ;
                IsValidEmail = false;
                return;
            }
            if (!Email.Contains("@"))
            {
                EmailErrorMessage = _localizationService.GetString("email_should_contain_at");
                IsValidEmail = false;
                return;
            }

            var result = await _accountService.CheckIfEmailExist(Email);
            if (result)
            {
                EmailErrorMessage = _localizationService.GetString("email_exist_in_system");
                IsValidEmail = false;
            }

            IsValidEmail = true;
        }

        public void ValidPassword()
        {
            if (!string.IsNullOrWhiteSpace(Password) && Password.Length < 7)
            {
                IsValidPassword = false;
                PasswordErrorMessage = _localizationService.GetString("password_should_contain_more_letters");
                return;
            }

            if (!Password.Any(char.IsDigit))
            {
                IsValidPassword = false;
                PasswordErrorMessage = _localizationService.GetString("password_should_contain_digit");
                return;
            }

            if (!Password.Any(char.IsUpper))
            {
                IsValidPassword = false;
                PasswordErrorMessage = _localizationService.GetString("password_should_contain_upper_letter");
                return;
            }

            //if(!Password.Any(char.))

            IsValidPassword = true;
            ValidRepeatablePassword();
        }

        public void ValidRepeatablePassword()
        {
            IsValidRepeatablePassword = true;
            var messeageError = string.Empty;
            if (Password != RepeatablePassword)
            {
                IsValidRepeatablePassword = false;
                messeageError = _localizationService.GetString("repeatable_password_should_be_same");
            }
            if (string.IsNullOrWhiteSpace(RepeatablePassword))
            {
                IsValidRepeatablePassword = false;
                messeageError = _localizationService.GetString("repeatable_password_could_not_be_emtpy");
            }

            RepeatablePasswordErrorMessage = messeageError;
        }

        private async Task SubmitRegistrationForm()
        {
            if (IsValidForm)
            {
                var result = await _accountService.RegisterUser(Name, Email, Password);

                if (result)
                {
                    //TODO: create Core of app, implement removing history
                    //ShowViewModelAndRemoveHistory<GamerHubViewModel>();
                }
            }
        }
    }
}
