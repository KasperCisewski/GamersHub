using System;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Commands
{
    public class AddGameToWishListCommand : ICommand
    {
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
    }
}