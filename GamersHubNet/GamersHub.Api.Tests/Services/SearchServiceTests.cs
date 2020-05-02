using System;
using FluentAssertions;
using GamersHub.Api.Services;
using GamersHub.Shared.Data.Enums;
using Xunit;

namespace GamersHub.Api.Tests.Services
{
    public class SearchServiceTests
    {
        private readonly ISearchService _searchService;

        public SearchServiceTests()
        {
            var dataContext = InMemoryFixture.Context;
            _searchService = new SearchService(dataContext);
        }

        [Fact]
        public async void SearchGamesByCategory_ForProperGameCategory_ShouldReturnGames()
        {
            var games = await _searchService.SearchGamesByCategory(GameCategory.Action, 0, 1);

            games.Count.Should().Be(1);
        }

        [Fact]
        public async void SearchGames_ForProperSearchText_ShouldReturnGames()
        {
            var games = await _searchService.SearchGames("Lorem", 0, 1);

            games.Count.Should().Be(1);
        }

        [Fact]
        public async void SearchGames_ForInvalidSearchText_ShouldReturnEmptyList()
        {
            var games = await _searchService.SearchGames("Test", 0, 1);

            games.Count.Should().Be(0);
        }

        [Fact]
        public async void SearchUsers_ForInvalidSearchText_ShouldReturnEmptyList()
        {
            var games = await _searchService.SearchUsers("Test", Guid.NewGuid(), 0, 1);

            games.Count.Should().Be(0);
        }

        [Fact]
        public async void SearchUsers_ForProperSearchText_ShouldReturnUsers()
        {
            var games = await _searchService.SearchUsers("cis", Guid.NewGuid(), 0, 1);

            games.Count.Should().Be(1);
        }
    }
}
