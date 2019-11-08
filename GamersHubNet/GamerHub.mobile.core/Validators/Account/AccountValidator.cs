using System.Linq;
using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services;
using GamerHub.mobile.core.Services.Account;

namespace GamerHub.mobile.core.Validators.Account
{
    public class AccountValidator : IAccountValidator
    {
        private readonly IAccountService _accountService;
        private readonly ILocalizationService _localizationService;

        public AccountValidator(
            IAccountService accountService,
            ILocalizationService localizationService)
        {
            _accountService = accountService;
            _localizationService = localizationService;
        }

        public async Task<ValidationResult> ValidEmail(string email)
        {
            var validationResult = new ValidationResult();

            if (!string.IsNullOrWhiteSpace(email) && email.Length < 3)
            {
                validationResult.ErrorMessage = _localizationService.GetString("email_is_too_short"); ;
                validationResult.IsValidate = false;
                return validationResult;
            }
            if (!email.Contains("@"))
            {
                validationResult.ErrorMessage = _localizationService.GetString("email_should_contain_at");
                validationResult.IsValidate = false;
                return validationResult;
            }

            var result = await _accountService.CheckIfEmailExist(email);
            if (result)
            {
                validationResult.ErrorMessage = _localizationService.GetString("email_exist_in_system");
                validationResult.IsValidate = false;
                return validationResult;
            }

            validationResult.IsValidate = true;
            return validationResult;
        }

        public async Task<ValidationResult> ValidName(string name)
        {
            var validationResult = new ValidationResult();
            if (!string.IsNullOrWhiteSpace(name) && name.Length < 3)
            {
                validationResult.ErrorMessage = _localizationService.GetString("name_is_too_short"); ;
                validationResult.IsValidate = false;
                return validationResult;
            }

            var result = await _accountService.CheckIfNameExist(name);
            if (result)
            {
                validationResult.ErrorMessage = _localizationService.GetString("name_exist_in_system");
                validationResult.IsValidate = false;
                return validationResult;
            }

            validationResult.IsValidate = true;
            return validationResult;
        }

        public ValidationResult ValidPassword(string password)
        {
            var validationResult = new ValidationResult();

            if (!string.IsNullOrWhiteSpace(password) && password.Length < 7)
            {
                validationResult.IsValidate = false;
                validationResult.ErrorMessage = _localizationService.GetString("password_should_contain_more_letters");
                return validationResult;
            }

            if (!password.Any(char.IsDigit))
            {
                validationResult.IsValidate = false;
                validationResult.ErrorMessage = _localizationService.GetString("password_should_contain_digit");
                return validationResult;
            }

            if (!password.Any(char.IsUpper))
            {
                validationResult.IsValidate = false;
                validationResult.ErrorMessage = _localizationService.GetString("password_should_contain_upper_letter");
                return validationResult;
            }

            if (password.All(char.IsLetterOrDigit))
            {
                validationResult.IsValidate = false;
                validationResult.ErrorMessage = _localizationService.GetString("password_should_contain_non_alphanumeric_char");
                return validationResult;
            }

            validationResult.IsValidate = true;
            return validationResult;
        }

        public ValidationResult ValidRepeatablePassword(string password, string repeatablePassword)
        {
            var validationResult = new ValidationResult();
            validationResult.IsValidate = true;
            validationResult.ErrorMessage = string.Empty;

            if (password != repeatablePassword)
            {
                validationResult.IsValidate = false;
                validationResult.ErrorMessage = _localizationService.GetString("repeatable_password_should_be_same");
            }
            if (string.IsNullOrWhiteSpace(repeatablePassword))
            {
                validationResult.IsValidate = false;
                validationResult.ErrorMessage = _localizationService.GetString("repeatable_password_could_not_be_emtpy");
            }

            return validationResult;
        }
    }
}
