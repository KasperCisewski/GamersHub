using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.Services
{
    public interface IGlobalStateService 
    {
        AuthSuccessResponse UserData { set; get; }
    }
}
