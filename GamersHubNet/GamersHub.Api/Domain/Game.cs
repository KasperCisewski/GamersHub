using GamersHub.Shared.Data.Enum;
using System;
using System.Collections.Generic;

namespace GamersHub.Api.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<GameOffer> GameOffers { get; set; }
        public List<GameImage> GameImages { get; set; }
        public Guid CoverImageId { get; set; }
        public GameImage CoverGameImage { get; set; }
        public List<Video> Videos { get; set; }
        public GameCategory GameCategory { get; set; }
    }
}
