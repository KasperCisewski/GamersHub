using GamerHub.mobile.core.Models.LoginAndRegistration;
using GamerHub.mobile.core.Services;
using GamerHub.mobile.core.Services.Account;
using GamerHub.mobile.core.ViewModels.Base;
using System.ComponentModel;
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
            if (parameter.UserEmailOrName.Contains("@"))
            {
                Email = parameter.UserEmailOrName;
            }
            else
            {
                Name = parameter.UserEmailOrName;
            }
        }

        public async Task ValidName()
        {
            throw new NotImplementedException();
        }

        public async Task ValidEmail()
        {
            IsValidEmail = false;
            EmailErrorMessage = string.Empty;
            if (Email != null)
            {
                if (!Email.Contains("@"))
                {
                    EmailErrorMessage =
                        LoginAndRegisterResources.NotValidEmailByNotContainAt;
                    IsValidEmail = true;
                    return;
                }

                if (Email.Length < 3)
                {
                    EmailErrorMessage =
                        LoginAndRegisterResources.EmailIsTooShort;
                    IsValidEmail = true;
                    return;
                }

                var result = await _userService.ValidateEmailByCheckIfExistInApp(Email);
                if (result)
                {
                    EmailErrorMessage =
                        LoginAndRegisterResources.EmailAlreadyIsInApplication;
                    IsValidEmail = true;
                }
            }
        }

        public void ValidPassword()
        {
            IsValidPassword = true;
            if (!string.IsNullOrWhiteSpace(Password) && Password.Length)
        }

        public void ValidRepeatablePassword()
        {
            IsValidRepeatablePassword = true;
            var messeageError = string.Empty;
            if (Password != RepeatablePassword)
            {
                IsValidRepeatablePassword = false;
                messeageError = _localizationService.GetString("");
            }
            if (string.IsNullOrWhiteSpace(RepeatablePassword))
            {
                IsValidRepeatablePassword = false;
                messeageError = _localizationService.GetString("");
            }

            RepeatablePasswordErrorMessage = messeageError;
        }
    }
}
