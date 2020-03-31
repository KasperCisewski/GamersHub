using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.PythonScripts;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.CommandHandlers
{
    internal class GetRecommendedGamesCommandHandler : ICommandHandler<GetRecommendedGamesCommand, IReadOnlyCollection<GameWithImageResponse>>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public GetRecommendedGamesCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<GameWithImageResponse>>> HandleAsync(GetRecommendedGamesCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<GameWithImageResponse>>();
            }

            var userId = command.UserId ?? command.CurrentUserId;

            var yesterday = DateTime.Now.AddDays(-1);
            var existingActualRecommendation = await _dataContext.GamesRecommendations
                .AsNoTracking()
                .Include(x => x.RecommendedGames)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GeneratedAt > yesterday);

            if (existingActualRecommendation != null)
            {
                var recommendedGamesIds = existingActualRecommendation.RecommendedGames.Select(x => x.GameId);
                var games = await _dataContext.Games
                    .Include(x => x.CoverGameImage)
                    .Where(x => recommendedGamesIds.Contains(x.Id))
                    .ToListAsync();

                return games.Select(x => new GameWithImageResponse
                {
                    Category = x.GameCategory,
                    Id = x.Id,
                    ImageBytes = x.CoverGameImage.Data.ToList(),
                    Title = x.Name
                })
                .ToList()
                .ToSuccessfulResult();
            }

            PythonScriptRunner.RunScript("PythonScripts/recommender.py", userId.ToString());

            var lines = File.ReadAllLines("list_of_games.txt");

            var recommendedGames = new List<Game>();

            foreach (var line in lines)
            {
                var game = await _dataContext.Games
                    .Include(x => x.CoverGameImage)
                    .SingleOrDefaultAsync(x => x.Name == line);

                if (game != null)
                    recommendedGames.Add(game);
            }

            var gamesRecommendation = new GamesRecommendation
            {
                GeneratedAt = DateTime.Now,
                UserId = userId,
                RecommendedGames = recommendedGames
                    .Select(x => new RecommendationEntry { Game = x }).ToList()
            };

            _dataContext.GamesRecommendations.Add(gamesRecommendation);
            await _dataContext.SaveChangesAsync();

            return recommendedGames.Select(x => new GameWithImageResponse
            {
                Category = x.GameCategory,
                Id = x.Id,
                ImageBytes = x.CoverGameImage.Data.ToList(),
                Title = x.Name
            })
            .ToList()
            .ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetRecommendedGamesCommand query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
