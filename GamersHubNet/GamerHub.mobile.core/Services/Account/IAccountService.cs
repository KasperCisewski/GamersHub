using System.Threading.Tasks;
using GamerHub.mobile.core.Models.Db;

namespace GamerHub.mobile.core.Services.Account
{
    public interface IAccountService
    {
        Task<bool> LogInUserAsync(string userName, string password);
        Task<bool> CheckIfNameExist(string name);
        Task<bool> CheckIfEmailExist(string email);
        Task<bool> RegisterUser(string userName, string email, string password);
        Task<bool> LogInByToken(UserCredentialsModel credentialsStoredInDb);
    }
}
