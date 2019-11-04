using GamerHub.mobile.core.Services.Account;
using GamerHub.mobile.core.ViewModels.Base;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;

        public LoginViewModel(
            IAccountService accountService
            )
        {
            _accountService = accountService;
        }

        private async Task TryToLogIntoApp()
        {
            var result = await _accountService.LogInUserAsync(Login, Password);
        }
    }
}
