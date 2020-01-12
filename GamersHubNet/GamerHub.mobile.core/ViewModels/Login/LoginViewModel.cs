using GamerHub.mobile.core.Models.LoginAndRegistration;
using GamerHub.mobile.core.Services.Account;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Home;
using GamerHub.mobile.core.ViewModels.Registration;
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

        private async Task GoToRegistrationView()
        {
            await ShowViewModel<RegistrationViewModel, LoginModel>(new LoginModel
            {
                UserEmailOrName = Login
            });
        }

        private async Task TryToLogIntoApp()
        {
            var result = await _accountService.LogInUserAsync(Login, Password);

            if (result)
            {
                await ShowViewModelAndRemoveHistory<HomeViewModel>();
            }
        }
    }
}
