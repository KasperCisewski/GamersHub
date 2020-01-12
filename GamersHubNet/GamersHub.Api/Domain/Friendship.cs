using System;

namespace GamersHub.Api.Domain
{
    public class Friendship
    {
        public Guid CurrentUserId { get; set; }
        public Guid FriendId{ get; set; }
    }
}
