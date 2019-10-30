using System.Collections.Generic;

namespace GamersHub.Api.Contracts.Responses
{
    public class AuthFailureResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
