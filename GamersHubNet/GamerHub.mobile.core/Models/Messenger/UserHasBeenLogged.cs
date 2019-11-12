using GamersHub.Shared.Contracts.Responses;
using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.core.Models.Messenger
{
    public class UserHasBeenLogged : MvxMessage
    {
        public AuthSuccessResponse User { get; set; }

        public UserHasBeenLogged(object sender, AuthSuccessResponse user) : base(sender)
        {
            User = user;
        }
    }
}
