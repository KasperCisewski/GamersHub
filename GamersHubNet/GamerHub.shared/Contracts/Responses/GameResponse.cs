using System;
using GamersHub.Shared.Data.Enums;

namespace GamersHub.Shared.Contracts.Responses
{
    public class GameResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public GameCategory Category { get; set; }
    }
}
