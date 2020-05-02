using System;
using System.Linq;
using FluentAssertions;
using GamersHub.Api.Data;
using GamersHub.Api.Services;
using Xunit;

namespace GamersHub.Api.Tests.Services
{
    public class FriendServiceTests
    {
        private readonly DataContext _dataContext;

        public FriendServiceTests()
        {
            _dataContext = InMemoryFixture.Context;
        }

        [Fact]
        public async void AddFriend_ForProperGuids_ShouldAddNewFriendShips()
        {
            var service = new FriendService(_dataContext);
            var currentUserGuid = Guid.Parse("852cd5f9-083e-464e-86e1-f2631eb88573");
            var newFriendId = Guid.Parse("475d30dc-6e0f-4369-bf15-37f4f8873215");

            await service.AddFriend(currentUserGuid, newFriendId);

            _dataContext.Friendships.FirstOrDefault(x => x.CurrentUserId == currentUserGuid && x.FriendId == newFriendId).Should().NotBeNull();
            _dataContext.Friendships.FirstOrDefault(x => x.CurrentUserId == newFriendId && x.FriendId == currentUserGuid).Should().NotBeNull();
        }

        [Fact]
        public async void DeleteFriend_ForProperGuids_ShouldDeleteFriendships()
        {
            var service = new FriendService(_dataContext);
            var friendOneId = Guid.Parse("44889f11-b43e-476f-8cba-e9bbbf5d2b86");
            var friendTwoId = Guid.Parse("c47d5746-461f-461a-9f99-88e5dd725c1a");

            await service.DeleteFriend(friendOneId, friendTwoId);

            _dataContext.Friendships.FirstOrDefault(x => x.CurrentUserId == friendOneId && x.FriendId == friendTwoId).Should().BeNull();
            _dataContext.Friendships.FirstOrDefault(x => x.CurrentUserId == friendTwoId && x.FriendId == friendOneId).Should().BeNull();
        }

        [Fact]
        public async void GetFriends_ForProperUserGuid_ShouldReturnUsersFriends()
        {
            var service = new FriendService(_dataContext);
            var userId = Guid.Parse("44889f11-b43e-476f-8cba-e9bbbf5d2b86");

            var friends = await service.GetFriends(userId);

            friends.Count.Should().Be(1);
            friends.First().Id.Should().Be(Guid.Parse("475d30dc-6e0f-4369-bf15-37f4f8873215"));
        }
    }
}
