using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.GamesVault
{
    public partial class GamesVaultViewModel : BaseViewModel<ProfileUserModel>
    {
        public override void Prepare(ProfileUserModel parameter)
        {
            IsOtherUserProfile = parameter.UserId != null;
            UserId = IsOtherUserProfile ? parameter.UserId : null;
        }
    }
}
