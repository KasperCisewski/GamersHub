using System;
using System.Collections.Generic;
using FluentAssertions;
using GamersHub.Api.Data;
using GamersHub.Api.Services;
using GamersHub.Shared.Contracts.Responses;
using Moq;
using Xunit;

namespace GamersHub.Api.Tests.Services
{
    public class GameServiceTests
    {
        private readonly DataContext _dataContext;

        public GameServiceTests()
        {
            _dataContext = InMemoryFixture.Context;
        }

        [Fact]
        public async void GetFullGameDescription_ForProperGameId_ShouldReturnGameData()
        {
            var service = new GameService(_dataContext);
            var gameId = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113");

            var result = await service.GetFullGameDescription(gameId, null);

            result.Should().BeEquivalentTo(new FullGameDescriptionResponse
            {
                Description = "Lorem ipsum",
                GeneralImage = new List<byte>() { It.IsAny<byte>()},
                Title = "Lorem",
                ReleaseDate = DateTime.Parse("01/01/2020")
            });
        }
    }
}
