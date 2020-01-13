using System;

namespace GamersHub.Api.Domain
{
    public class WishListEntry
    {
        public Guid UserId { get; set; }
        public GamersHubUser User { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
