using System.Threading.Tasks;

namespace GamerHub.mobile.core.Services.Account
{
    public interface IAccountService
    {
        Task<bool> LogInUserAsync(string userName, string password);
    }
}
