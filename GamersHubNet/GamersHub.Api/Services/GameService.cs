using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("GamersHub.Api.Tests")]
namespace GamersHub.Api.Services
{
    public interface IGameService
    {
        Task<FullGameDescriptionResponse> GetFullGameDescription(Guid gameId, Guid? userId);
        Task<string> GetGameVideoUrl(Guid gameId);
        Task<IReadOnlyCollection<GameWithImageResponse>> GetHomeScreenGames(HomeGamesCategory homeGamesCategory);
        Task<IReadOnlyCollection<ScreenShotResponse>> GetScreenshots(Guid gameId);
        Task AddGameToVault(Guid gameId, Guid userId);
        Task AddGameToWishList(Guid gameId, Guid userId);
        Task DeleteGameFromWishList(Guid gameId, Guid userId);
        Task DeleteGameFromVault(Guid gameId, Guid userId);
    }

    internal class GameService : IGameService
    {
        private readonly DataContext _dataContext;

        public GameService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<FullGameDescriptionResponse> GetFullGameDescription(Guid gameId, Guid? userId)
        {
            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.CoverGameImage)
                .FirstOrDefaultAsync(x => x.Id == gameId);

            var model = new FullGameDescriptionResponse
            {
                Description = game.Description,
                GeneralImage = game.CoverGameImage.Data.ToList(),
                Title = game.Name,
                ReleaseDate = game.ReleaseDate,
            };

            if (userId != null)
            {
                var user = await _dataContext.Users
                    .Include(x => x.Games)
                    .Include(x => x.WishList)
                    .SingleOrDefaultAsync(x => x.Id == userId);

                model.UserHasGameInVault = user.Games.Any(x => x.GameId == gameId);
                model.UserHasGameOnWishList = user.WishList.Any(x => x.GameId == gameId);
            }

            return model;
        }

        public async Task<string> GetGameVideoUrl(Guid gameId)
        {
            return (await _dataContext.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == gameId))
                .VideoUrl;
        }

        public async Task<IReadOnlyCollection<GameWithImageResponse>> GetHomeScreenGames(HomeGamesCategory homeGamesCategory)
        {
            var games = homeGamesCategory switch
            {
                HomeGamesCategory.ComingSoon => await _dataContext.Games.AsNoTracking()
                    .Skip(0)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.BrandNew => await _dataContext.Games.AsNoTracking()
                    .Skip(10)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.Hottest => await _dataContext.Games.AsNoTracking()
                    .Skip(20)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.OnSale => await _dataContext.Games.AsNoTracking()
                    .Skip(30)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                _ => new List<Game>()
            };

            return games
                .Select(x => new GameWithImageResponse
                {
                    Id = x.Id,
                    Category = x.GameCategory,
                    Title = x.Name,
                    ImageBytes = x.CoverGameImage.Data.ToList()
                })
                .ToList();
        }

        public async Task<IReadOnlyCollection<ScreenShotResponse>> GetScreenshots(Guid gameId)
        {
            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.GameImages)
                .FirstOrDefaultAsync(x => x.Id == gameId);

            return game.GameImages
                .Select(x => new ScreenShotResponse { ImageContent = x.Data.ToList() })
                .ToList();
        }

        public async Task AddGameToVault(Guid gameId, Guid userId)
        {
            var game = await _dataContext.Games.FindAsync(gameId);
            var user = await _dataContext.Users.FindAsync(userId);

            var vaultEntry = new UserGame()
            {
                Game = game,
                User = user,
            };

            user.Games.Add(vaultEntry);

            await _dataContext.SaveChangesAsync();
        }

        public async Task AddGameToWishList(Guid gameId, Guid userId)
        {
            var game = await _dataContext.Games.FindAsync(gameId);
            var user = await _dataContext.Users.FindAsync(userId);

            var wishListEntry = new WishListEntry()
            {
                Game = game,
                User = user,
            };

            user.WishList.Add(wishListEntry);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteGameFromWishList(Guid gameId, Guid userId)
        {
            var user = await _dataContext.Users
                .Include(x => x.Games)
                .FirstAsync(x => x.Id == userId);

            var userGame = user.WishList.First(x => x.GameId == gameId);

            user.WishList.Remove(userGame);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteGameFromVault(Guid gameId, Guid userId)
        {
            var user = await _dataContext.Users
                .Include(x => x.Games)
                .FirstAsync(x => x.Id == userId);

            var userGame = user.Games.First(x => x.GameId == gameId);

            user.Games.Remove(userGame);

            await _dataContext.SaveChangesAsync();
        }
    }
}
