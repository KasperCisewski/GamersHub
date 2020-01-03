using System;
using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public List<byte> ProfileImageContent { get; set; }
    }
}
