using System.Threading.Tasks;

namespace GamerHub.mobile.core.Services.Account
{
    public interface IAccountService
    {
        Task<bool> LogInUserAsync(string userName, string password);
        Task<bool> ValidateEmailByCheckIfExistInApp(string name);
        Task<bool> ValidateNameByCheckIfExistInApp(string email);
        Task RegisterUser(string name, string email, string password);
    }
}
