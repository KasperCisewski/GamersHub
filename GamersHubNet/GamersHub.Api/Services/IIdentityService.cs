using GamersHub.Api.Domain;
using System.Threading.Tasks;

namespace GamersHub.Api.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}
