using GamerHub.mobile.core.Models;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.Validators.Account
{
    public interface IAccountValidator
    {
        Task<ValidationResult> ValidName(string name);
        Task<ValidationResult> ValidEmail(string email);
        ValidationResult ValidPassword(string password);
        ValidationResult ValidRepeatablePassword(string password, string repeatablePassword);
    }
}
