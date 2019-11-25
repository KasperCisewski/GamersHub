using System;
using GamersHub.Shared.Data.Enums;

namespace GamersHub.Shared.Model
{
    public abstract class GameModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public GameCategory Category { get; set; }
    }
}
