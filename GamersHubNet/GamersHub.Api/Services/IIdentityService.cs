using GamersHub.Api.Domain;
using System.Threading.Tasks;

namespace GamersHub.Api.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, string username);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        public bool UserWithEmailExists(string email);
        public bool UserWithUsernameExists(string username);
    }
}
