using System;
using GamersHub.Api.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace GamersHub.Api.Data
{
    public class DataContext : IdentityDbContext<GamersHubUser, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        protected DataContext()
        {
            //needed for mocking db context to work
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Metatag> Metatags { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameOffer> GameOffers { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<GeneratedHeatmap> GeneratedHeatMaps { get; set; }
        public DbSet<GamesRecommendation> GamesRecommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
