using GamerHub.shared.Data.Enums;
using System;

namespace GamerHub.shared.Contracts.Requests
{
    public abstract class GameModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public GameCategory Category { get; set; }
    }
}
