using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class AuthFailureResponse : AuthResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
