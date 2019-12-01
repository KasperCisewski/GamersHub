using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GamersHub.Api.Domain
{
    public class GamersHubUser : IdentityUser
    {
        public List<Game> WishList { get; set; }
        public List<Game> Games { get; set; }
        public List<GamersHubUser> Friends { get; set; }
    }
}
