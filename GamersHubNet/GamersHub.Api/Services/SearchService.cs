using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.Services
{
    public interface ISearchService
    {
        Task<IReadOnlyCollection<GameWithImageResponse>> SearchGamesByCategory(GameCategory gameCategory, int skip, int take);
        Task<IReadOnlyCollection<GameWithImageResponse>> SearchGames(string searchText, int skip, int take);
        Task<IReadOnlyCollection<UserProfileResponse>> SearchUsers(string searchText, Guid userId, int skip, int take);
    }

    internal class SearchService : ISearchService
    {
        private readonly DataContext _dataContext;

        public SearchService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IReadOnlyCollection<GameWithImageResponse>> SearchGamesByCategory(GameCategory gameCategory, int skip = 0, int take = 0)
        {
            var games = await _dataContext.Games
                .AsNoTracking()
                .Where(x => x.GameCategory == gameCategory)
                .Include(x => x.CoverGameImage)
                .Skip(skip)
                .Take(take == default ? 10 : take)
                .ToArrayAsync();

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

        public async Task<IReadOnlyCollection<GameWithImageResponse>> SearchGames(string searchText, int skip = 0, int take = 0)
        {
            var games = await _dataContext.Games
                .AsNoTracking()
                .Where(x => x.Name
                    .Contains(searchText))
                .Include(x => x.CoverGameImage)
                .Skip(skip)
                .Take(take == default ? 10 : take)
                .ToListAsync();

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

        public async Task<IReadOnlyCollection<UserProfileResponse>> SearchUsers(string searchText, Guid userId, int skip = 0, int take = 0)
        {
            var users = await _dataContext.Users
                .AsNoTracking()
                .Where(x => x.UserName.Contains(searchText))
                .Skip(skip)
                .Take(take == default ? 10 : take)
                .Select(x => new UserProfileResponse
                {
                    Id = x.Id,
                    ProfileImageContent = null,
                    UserName = x.UserName,
                }).ToListAsync();

            foreach (var user in users)
            {
                user.IsUserFriend = await _dataContext.Friendships
                    .AnyAsync(x => x.CurrentUserId == userId && x.FriendId == user.Id);
            }

            return users.ToList();
        }
    }
}
