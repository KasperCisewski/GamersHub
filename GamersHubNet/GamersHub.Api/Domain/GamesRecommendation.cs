using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;

namespace GamersHub.Api.Domain
{
    public class GamesRecommendation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime GeneratedAt { get; set; }
        public List<RecommendationEntry> RecommendedGames { get; set; }

        public GamesRecommendation()
        {
            Guid.NewGuid();
        }
    }
}
