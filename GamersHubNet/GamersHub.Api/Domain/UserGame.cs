using System;

namespace GamersHub.Api.Domain
{
    public class UserGame
    {
        public Guid UserId { get; set; }
        public GamersHubUser User { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
