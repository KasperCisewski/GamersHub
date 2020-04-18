using System;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Commands
{
    public class ChangeProfileImageCommand : ICommand
    {
        public byte[] ImageContent { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}