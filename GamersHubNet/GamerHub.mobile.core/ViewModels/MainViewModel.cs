using System;
using System.Threading.Tasks;
using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services;
using GamerHub.mobile.core.Services.Account;
using GamerHub.mobile.core.Services.Db;
using GamerHub.mobile.core.ViewModels.Base;
using GamerHub.mobile.core.ViewModels.CoreApp.Home;
using GamerHub.mobile.core.ViewModels.Login;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _userLoggedInSubscription;
        private AuthSuccessResponse _user = new AuthSuccessResponse();

        private readonly IMvxNavigationService _navigationService;
        private readonly ISqlLiteService _sqlLiteService;
        private readonly IGlobalStateService _globalStateService;
        private readonly IAccountService _accountService;

        public MainViewModel(
            IMvxNavigationService navigationService,
            ISqlLiteService sqlLiteService,
            IGlobalStateService globalStateService,
            IAccountService accountService)
        {
            _navigationService = navigationService;
            _sqlLiteService = sqlLiteService;
            _globalStateService = globalStateService;
            _accountService = accountService;
        }

        public AuthSuccessResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public override void Prepare()
        {
            base.Prepare();
            ConfigureEvents();
        }

        public async Task NavigateToFirstViewModel()
        {
            var credentialsStoredInDb = _sqlLiteService.GetCredentialsStoredInDb();
            if (credentialsStoredInDb != null)
            {
                var result = credentialsStoredInDb.ExpiryDate.AddMinutes(30) > DateTime.UtcNow || await _accountService.LogInByToken(credentialsStoredInDb);

                if (result)
                {
                    _globalStateService.UserData = new AuthSuccessResponse
                    {
                        ExpiryDate = credentialsStoredInDb.ExpiryDate,
                        RefreshToken = credentialsStoredInDb.RefreshToken,
                        Token = credentialsStoredInDb.Token
                    };
                    await _navigationService.Navigate<HomeViewModel>();
                }
                else
                {
                    await _navigationService.Navigate<LoginViewModel>();
                }
            }
            else
            {
                await _navigationService.Navigate<LoginViewModel>();
            }
        }

        private void ConfigureEvents()
        {
            _userLoggedInSubscription = Messenger.Subscribe<UserHasBeenLogged>(m =>
            {
                User = m.User;
            });
        }
    }
}
