using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class AuthFailureResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
