﻿using GamersHub.Shared.Data.Enums;
using System;
using System.Collections.Generic;

namespace GamersHub.Api.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<GameImage> GameImages { get; set; }
        public GameImage CoverGameImage { get; set; }
        public string VideoUrl { get; set; }
        public GameCategory GameCategory { get; set; }
        public ICollection<UserGame> UserGames { get; set; }
        public ICollection<WishListEntry> WishlistEntries { get; set; }
    }
}
