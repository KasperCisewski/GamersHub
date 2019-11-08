using GamerHub.mobile.core.Models.LoginAndRegistration;
using GamerHub.mobile.core.Services.Account;
using GamerHub.mobile.core.Validators.Account;
using GamerHub.mobile.core.ViewModels.Base;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.ViewModels.Registration
{
    public partial class RegistrationViewModel : BaseViewModel<LoginModel>
    {
        private readonly IAccountValidator _accountValidator;
        private readonly IAccountService _accountService;

        public RegistrationViewModel(
            IAccountValidator accountValidator,
            IAccountService accountService
            )
        {
            _accountValidator = accountValidator;
            _accountService = accountService;
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
            var validationResult = await _accountValidator.ValidName(Name);
            IsValidName = validationResult.IsValidate;
            NameErrorMessage = validationResult.ErrorMessage;
        }

        public async Task ValidEmail()
        {
            var validationResult = await _accountValidator.ValidName(Name);
            IsValidEmail = validationResult.IsValidate;
            EmailErrorMessage = validationResult.ErrorMessage;
        }

        public void ValidPassword()
        {
            var validationResult = _accountValidator.ValidPassword(Password);
            IsValidPassword = validationResult.IsValidate;
            PasswordErrorMessage = validationResult.ErrorMessage;
            if (IsValidPassword)
            {
                ValidRepeatablePassword();
            }
        }

        public void ValidRepeatablePassword()
        {
            var validationResult = _accountValidator.ValidRepeatablePassword(Password, RepeatablePassword);
            IsValidRepeatablePassword = validationResult.IsValidate;
            RepeatablePasswordErrorMessage = validationResult.ErrorMessage;
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
