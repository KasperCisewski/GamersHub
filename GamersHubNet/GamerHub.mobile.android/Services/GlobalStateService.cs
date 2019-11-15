using GamerHub.mobile.core.Models.Messenger;
using GamerHub.mobile.core.Services;
using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.android.Services
{
    public class GlobalStateService : IGlobalStateService
    {
        private static readonly object Lock = new object();
        private readonly IMvxMessenger _mvxMessenger;

        public GlobalStateService(
            IMvxMessenger mvxMessenger)
        {
            _mvxMessenger = mvxMessenger;
        }

        private AuthSuccessResponse _userData;
        public AuthSuccessResponse UserData
        {
            get => _userData ?? new AuthSuccessResponse();
            set
            {
                lock (Lock)
                {
                    if (value == null)
                    {
                        _userData = new AuthSuccessResponse();
                    }
                    else
                    {
                        _userData = value;
                        _mvxMessenger.Publish(new UserHasBeenLogged(this, _userData));
                    }
                }
            }
        }
    }
}