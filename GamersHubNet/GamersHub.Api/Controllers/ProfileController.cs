using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Responses;
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
            if (userId == null)
                userId = HttpContext.GetUserId();

            var user = await _dataContext.Users.FindAsync(userId);

            return new UserProfile
            {
                Id = user.Id,
                UserName = user.UserName,
                //TODO 
                ProfileImageContent = null
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
                    UserName = x.UserName
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
    }
}