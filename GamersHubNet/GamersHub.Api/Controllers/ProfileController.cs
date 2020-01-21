using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.PythonScripts;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DataContext _dataContext;

        public ProfileController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet(ApiRoutes.Profile.GetUserProfileInformation)]
        [Authorize]
        public async Task<UserProfile> GetUserProfile(Guid? userId)
        {
            var currentUserId = HttpContext.GetUserId();
            if (userId == null)
                userId = currentUserId;

            var user = await _dataContext.Users.FindAsync(userId);

            var isFriend = await _dataContext.Friendships
                .AnyAsync(x => x.CurrentUserId == currentUserId && x.FriendId == userId);

            return new UserProfile
            {
                Id = user.Id,
                UserName = user.UserName,
                //TODO 
                ProfileImageContent = null,
                IsUserFriend = isFriend
            };
        }

        [HttpGet(ApiRoutes.Profile.GetUserFriends)]
        [Authorize]
        public async Task<IEnumerable<UserProfile>> GetUserFriends(Guid? userId)
        {
            if (userId == null)
                userId = HttpContext.GetUserId();

            var friendIds = _dataContext.Friendships
                .AsNoTracking()
                .Where(x => x.CurrentUserId == userId)
                .Select(x => x.FriendId);

            var friends = await _dataContext.Users
                .AsNoTracking()
                .Where(x => friendIds.Contains(x.Id))
                .Select(x => new UserProfile
                {
                    Id = x.Id,
                    ProfileImageContent = null,
                    UserName = x.UserName,
                    IsUserFriend = true
                }).ToListAsync();

            return friends;
        }

        [HttpGet(ApiRoutes.Profile.GetGamesInVault)]
        [Authorize]
        public async Task<IEnumerable<GameModelWithImage>> GetGamesInVault(Guid? userId)
        {
            if (userId == null)
                userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                    .Include(x => x.Games)
                    .SingleOrDefaultAsync(x => x.Id == userId);

            var userGames = user.Games.Select(x => x.GameId);

            var games = await _dataContext.Games
                .Include(x => x.CoverGameImage)
                .Where(x => userGames.Contains(x.Id))
                .ToListAsync();

            return games.Select(x => new GameModelWithImage
            {
                Category = x.GameCategory,
                Id = x.Id,
                ImageBytes = x.CoverGameImage.Data.ToList(),
                Title = x.Name
            });
        }

        [HttpGet(ApiRoutes.Profile.GetWishListGames)]
        [Authorize]
        public async Task<IEnumerable<GameModelWithImage>> GetGamesOnWishList(Guid? userId)
        {
            if (userId == null)
                userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                .Include(x => x.WishList)
                .SingleOrDefaultAsync(x => x.Id == userId);

            var userGames = user.WishList.Select(x => x.GameId);

            var games = await _dataContext.Games
                .Include(x => x.CoverGameImage)
                .Where(x => userGames.Contains(x.Id))
                .ToListAsync();

            return games.Select(x => new GameModelWithImage
            {
                Category = x.GameCategory,
                Id = x.Id,
                ImageBytes = x.CoverGameImage.Data.ToList(),
                Title = x.Name
            });
        }

        [HttpGet(ApiRoutes.Profile.GetUserGenres)]
        public async Task<IActionResult> GetUserGenres(Guid? userId)
        {
            if (userId == null)
                userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .SingleOrDefaultAsync(x => x.Id == userId);

            var userGames = user.Games.Select(x => x.GameId);

            var games = await _dataContext.Games
                .Where(x => userGames.Contains(x.Id))
                .ToListAsync();

            var countedGenres = games
                .GroupBy(x => x.GameCategory)
                .Select(g => new { GenreName = g.Key.ToString(), GenreCount = g.Count() })
                .ToDictionary(x => x.GenreName, x => x.GenreCount);

            Enum.GetNames(typeof(GameCategory)).ToImmutableList()
                .ForEach(x => countedGenres.TryAdd(x, 0));

            return Json(new { userId, genres = countedGenres });
        }

        [HttpGet(ApiRoutes.Profile.GetUserGamesNames)]
        public async Task<IActionResult> GetUserGamesNames(Guid? userId)
        {
            if (userId == null)
                userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                .Include(x => x.Games)
                    .ThenInclude(x => x.Game)
                .SingleOrDefaultAsync(x => x.Id == userId);

            var gamesNames = user.Games.Select(x => x.Game.Name);

            return Json(new { games = gamesNames });
        }

        [HttpPost(ApiRoutes.Profile.AddToFriendList)]
        [Authorize]
        public async Task<IActionResult> AddToFriendList([FromBody] AddDeleteFromFriendListRequest request)
        {
            var currentUserId = HttpContext.GetUserId();

            var userExists = await _dataContext.Users.AnyAsync(x => x.Id == request.UserId);

            if (!userExists)
                return BadRequest("User with given id does not exist");

            var friendship = new Friendship { CurrentUserId = currentUserId, FriendId = request.UserId };
            var friendshipReversed = new Friendship { CurrentUserId = request.UserId, FriendId = currentUserId };

            _dataContext.Friendships.Add(friendship);
            _dataContext.Friendships.Add(friendshipReversed);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete(ApiRoutes.Profile.DeleteFromFriendList)]
        [Authorize]
        public async Task<IActionResult> DeleteFromFriendList([FromBody] AddDeleteFromFriendListRequest request)
        {
            var currentUserId = HttpContext.GetUserId();

            var userExists = await _dataContext.Users.AnyAsync(x => x.Id == request.UserId);

            if (!userExists)
                return BadRequest("User with given id does not exist");

            var friendship = await _dataContext.Friendships
                .SingleOrDefaultAsync(x => x.CurrentUserId == currentUserId && x.FriendId == request.UserId);
            var friendshipReversed = await _dataContext.Friendships
                .SingleOrDefaultAsync(x => x.CurrentUserId == request.UserId && x.FriendId == currentUserId);

            _dataContext.Friendships.Remove(friendship);
            _dataContext.Friendships.Remove(friendshipReversed);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet(ApiRoutes.Profile.GetHeatMap)]
        [Authorize]
        public async Task<List<byte>> GetHeatMap(Guid? userId)
        {
            if (userId == null)
                userId = HttpContext.GetUserId();

            var existingActualHeatMap = await _dataContext.GeneratedHeatMaps
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId.Value && x.GeneratedAt > DateTime.Now.AddDays(-1));

            if (existingActualHeatMap != null)
            {
                return existingActualHeatMap.HeatMap.ToList();
            }

            PythonScriptRunner.RunScript("PythonScripts/heatmap.py", userId.Value.ToString());

            var fileInfo = new FileInfo("heatplot.png");

            var data = new byte[fileInfo.Length];

            await using (var fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }

            fileInfo.Delete();

            _dataContext.GeneratedHeatMaps.Add(new GeneratedHeatmap
                {HeatMap = data, UserId = userId.Value, GeneratedAt = DateTime.Now});

            await _dataContext.SaveChangesAsync();

            return data.ToList();
        }

        [HttpGet(ApiRoutes.Profile.GetRecommendedGames)]
        [Authorize]
        public async Task<IEnumerable<GameModelWithImage>> GetRecommendedGames(Guid? userId)
        {
            if (userId == null)
                userId = HttpContext.GetUserId();

            var yesterday = DateTime.Now.AddDays(-1);
            var existingActualRecommendation = await _dataContext.GamesRecommendations
                .AsNoTracking()
                .Include(x => x.RecommendedGames)
                .FirstOrDefaultAsync(x => x.UserId == userId.Value && x.GeneratedAt > yesterday);

            if (existingActualRecommendation != null)
            {
                var recommendedGamesIds = existingActualRecommendation.RecommendedGames.Select(x => x.GameId);
                var games = await _dataContext.Games
                    .Include(x => x.CoverGameImage)
                    .Where(x => recommendedGamesIds.Contains(x.Id))
                    .ToListAsync();

                return games.Select(x => new GameModelWithImage
                {
                    Category = x.GameCategory,
                    Id = x.Id,
                    ImageBytes = x.CoverGameImage.Data.ToList(),
                    Title = x.Name
                }); ;
            }

            PythonScriptRunner.RunScript("PythonScripts/recommender.py", userId.Value.ToString());

            var lines = System.IO.File.ReadAllLines("list_of_games.txt");

            var recommendedGames = new List<Game>();

            foreach (var line in lines)
            {
                var game = await _dataContext.Games
                    .Include(x => x.CoverGameImage)
                    .SingleOrDefaultAsync(x => x.Name == line);

                if(game != null)
                    recommendedGames.Add(game);
            }

            var gamesRecommendation = new GamesRecommendation
            {
                GeneratedAt = DateTime.Now,
                UserId = userId.Value,
                RecommendedGames = recommendedGames
                    .Select(x => new RecommendationEntry {Game = x}).ToList()
            };

            _dataContext.GamesRecommendations.Add(gamesRecommendation);
            await _dataContext.SaveChangesAsync();

            return recommendedGames.Select(x => new GameModelWithImage
            {
                Category = x.GameCategory,
                Id = x.Id,
                ImageBytes = x.CoverGameImage.Data.ToList(),
                Title = x.Name
            });
        }
    }
}