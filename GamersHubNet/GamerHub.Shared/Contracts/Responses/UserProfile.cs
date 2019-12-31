using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public List<byte> ProfileImageContent { get; set; }
    }
}
