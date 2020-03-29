using System;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Commands
{
    public class DeleteGameFromVaultCommand : ICommand
    {
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
    }
}