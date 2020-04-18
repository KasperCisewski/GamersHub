using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GamersHub.Api.Domain
{
    public class GamersHubUser : IdentityUser<Guid>
    {
        public ICollection<WishListEntry> WishList { get; set; }
        public ICollection<UserGame> Games { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
