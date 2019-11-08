using System.ComponentModel.DataAnnotations;

namespace GamersHub.Shared.Contracts.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
