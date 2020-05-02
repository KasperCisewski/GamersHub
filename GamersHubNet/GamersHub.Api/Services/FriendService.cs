using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Shared.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

[assembly:InternalsVisibleTo("GamersHub.Api.Tests")]
namespace GamersHub.Api.Services
{
    public interface IFriendService
    {
        Task AddFriend(Guid currentUserId, Guid newFriendId);
        Task DeleteFriend(Guid currentUserId, Guid friendId);
        Task<IReadOnlyCollection<UserProfileResponse>> GetFriends(Guid userId);
    }

    internal class FriendService : IFriendService
    {
        private readonly DataContext _dataContext;

        public FriendService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddFriend(Guid currentUserId, Guid newFriendId)
        {
            var friendship = new Friendship { CurrentUserId = currentUserId, FriendId = newFriendId };
            var friendshipReversed = new Friendship { CurrentUserId = newFriendId, FriendId = currentUserId };

            _dataContext.Friendships.Add(friendship);
            _dataContext.Friendships.Add(friendshipReversed);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteFriend(Guid currentUserId, Guid friendId)
        {
            var friendship = await _dataContext.Friendships
                .SingleOrDefaultAsync(x => x.CurrentUserId == currentUserId && x.FriendId == friendId);
            var friendshipReversed = await _dataContext.Friendships
                .SingleOrDefaultAsync(x => x.CurrentUserId == friendId && x.FriendId == currentUserId);

            _dataContext.Friendships.Remove(friendship);
            _dataContext.Friendships.Remove(friendshipReversed);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<UserProfileResponse>> GetFriends(Guid userId)
        {
            var friendsIds = _dataContext.Friendships
                .AsNoTracking()
                .Where(x => x.CurrentUserId == userId)
                .Select(x => x.FriendId);

            return await _dataContext.Users
                .AsNoTracking()
                .Where(x => friendsIds.Contains(x.Id))
                .Select(x => new UserProfileResponse
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    IsUserFriend = true
                }).ToListAsync();
        }
    }
}
