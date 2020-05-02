using System;

namespace GamersHub.Api.Domain
{
    public class GeneratedHeatmap
    {
        public Guid Id { get; set; }
        public byte[] HeatMap { get; set; }
        public Guid UserId { get; set; }
        public DateTime GeneratedAt { get; set; }
        public int GamesCount { get; set; }

        public GeneratedHeatmap()
        {
            Id = Guid.NewGuid();
        }
    }
}
