using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GamersHub.Api.Data;
using GamersHub.Api.Services;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Moq;
using Xunit;

namespace GamersHub.Api.Tests.Services
{
    public class GameServiceTests
    {
        private readonly IGameService _gameService;
        private readonly DataContext _dataContext;

        public GameServiceTests()
        {
            _dataContext = InMemoryFixture.Context;
            _gameService = new GameService(_dataContext);
        }

        [Fact]
        public async void GetFullGameDescription_ForProperGameId_ShouldReturnGameData()
        {
            var gameId = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113");

            var result = await _gameService.GetFullGameDescription(gameId, null);

            result.Should().BeEquivalentTo(new FullGameDescriptionResponse
            {
                Description = "Lorem ipsum",
                GeneralImage = new List<byte>() { It.IsAny<byte>()},
                Title = "Lorem",
                ReleaseDate = DateTime.Parse("01/01/2020")
            });
        }

        [Fact]
        public async void GetGameVideoUrl_ForProperGameId_ShouldReturnGameVideoUrlString()
        {
            var gameId = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113");

            var result = await _gameService.GetGameVideoUrl(gameId);

            result.Should().Be("https://www.youtube.com/watch?v=cG6dyEjgtIM");
        }

        [Fact]
        public async void GetHomeScreenGames_ForProperCategory_GamesListShouldNotBeEmpty()
        {
            var result = await _gameService.GetHomeScreenGames(HomeGamesCategory.ComingSoon);

            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetHomeScreenGames_ForInvalidCategory_GamesListShouldBeEmpty()
        {
            var result = await _gameService.GetHomeScreenGames((HomeGamesCategory)6);

            result.Count.Should().Be(0);
        }

        [Fact]
        public async void GetScreenshots_ForProperGameId_ShouldReturnScreenshots()
        {
            var result = await _gameService.GetScreenshots(Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113"));

            result.First().Should().BeEquivalentTo(new ScreenShotResponse { ImageContent = new List<byte> {1, 2 , 3 }});
        }

        [Fact]
        public async void AddGameToVault_ForProperGameAndUserId_ShouldAddGameToUsersVault()
        {
            var userId = Guid.Parse("852cd5f9-083e-464e-86e1-f2631eb88573");
            var gameId = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113");
            var userGamesCount = _dataContext.Users.First(x => x.Id == userId).Games.Count;

            await _gameService.AddGameToVault(gameId, userId);
            var newCount = _dataContext.Users.First(x => x.Id == userId).Games.Count;

            newCount.Should().Be(userGamesCount + 1);
        }

        [Fact]
        public async void AddGameToWishList_ForProperGameAndUserId_ShouldAddGameToUsersWishList()
        {
            var userId = Guid.Parse("852cd5f9-083e-464e-86e1-f2631eb88573");
            var gameId = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113");
            var userGamesCount = _dataContext.Users.First(x => x.Id == userId).WishList.Count;

            await _gameService.AddGameToWishList(gameId, userId);
            var newCount = _dataContext.Users.First(x => x.Id == userId).WishList.Count;

            newCount.Should().Be(userGamesCount + 1);
        }

        [Fact]
        public async void DeleteGameFromWishList_ForProperGameAndUserId_ShouldDeleteGameFromWishList()
        {
            var userId = Guid.Parse("475d30dc-6e0f-4369-bf15-37f4f8873215");
            var gameId = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113");
            var userGamesCount = _dataContext.Users.First(x => x.Id == userId).WishList.Count;

            await _gameService.DeleteGameFromWishList(gameId, userId);
            var newCount = _dataContext.Users.First(x => x.Id == userId).WishList.Count;

            newCount.Should().Be(userGamesCount - 1);
        }

        [Fact]
        public async void DeleteGameFromVault_ForProperGameAndUserId_ShouldDeleteGameFromVault()
        {
            var userId = Guid.Parse("475d30dc-6e0f-4369-bf15-37f4f8873215");
            var gameId = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113");
            var userGamesCount = _dataContext.Users.First(x => x.Id == userId).Games.Count;

            await _gameService.DeleteGameFromVault(gameId, userId);
            var newCount = _dataContext.Users.First(x => x.Id == userId).Games.Count;

            newCount.Should().Be(userGamesCount - 1);
        }
    }
}
