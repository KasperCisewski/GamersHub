using System;

namespace GamersHub.Api.Domain
{
    public class RecommendationEntry
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
