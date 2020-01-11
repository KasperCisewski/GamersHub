using System;

namespace GamersHub.Api.Domain
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public Guid UserId { get; set; }
        public GamersHubUser User { get; set; }
    }
}
