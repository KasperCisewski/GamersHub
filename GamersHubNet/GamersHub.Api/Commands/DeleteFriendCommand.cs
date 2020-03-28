using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Commands
{
    public class DeleteFriendCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
