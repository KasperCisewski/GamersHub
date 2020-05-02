using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Game;
using GamersHub.Api.Services;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.QueryHandlers.Game
{
    internal class GetHomeScreenGamesQueryHandler : IQueryHandler<GetHomeScreenGamesQuery, IReadOnlyCollection<GameWithImageResponse>>
    {
        private readonly IGameService _gameService;
        private readonly IValidator _validator;

        public GetHomeScreenGamesQueryHandler(
            IGameService gameService,
            IValidator validator)
        {
            _validator = validator;
            _gameService = gameService;
        }

        public async Task<IResult<IReadOnlyCollection<GameWithImageResponse>>> HandleAsync(GetHomeScreenGamesQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<GameWithImageResponse>>();
            }

            var games = await _gameService.GetHomeScreenGames(query.HomeGamesCategory);

            return games.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetHomeScreenGamesQuery query)
        {
            return _validator.ValidateAsync();
        }
    }
}
