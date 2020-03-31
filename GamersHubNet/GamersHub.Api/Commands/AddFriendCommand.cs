using System;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Commands
{
    public class AddFriendCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
