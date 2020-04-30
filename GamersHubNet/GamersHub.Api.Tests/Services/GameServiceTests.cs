using System;
using System.Collections.Generic;
using FluentAssertions;
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

        public GameServiceTests()
        {
            var dataContext = InMemoryFixture.Context;
            _gameService = new GameService(dataContext);
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
    }
}
