using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.ViewModels.Base;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _userLoggedInSubscription;
        private AuthSuccessResponse _user = new AuthSuccessResponse();

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

        private void ConfigureEvents()
        {
            _userLoggedInSubscription = Messenger.Subscribe<UserHasBeenLogged>(m =>
            {
                User = m.User;
            });
        }
    }
}
