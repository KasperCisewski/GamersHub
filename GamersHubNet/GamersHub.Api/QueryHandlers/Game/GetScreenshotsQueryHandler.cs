using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Game;
using GamersHub.Api.Services;
using GamersHub.Api.ValidationRules;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.QueryHandlers.Game
{
    internal class GetScreenshotsQueryHandler : IQueryHandler<GetScreenshotsQuery, IReadOnlyCollection<ScreenShotResponse>>
    {
        private readonly IGameService _gameService;
        private readonly IValidator _validator;

        public GetScreenshotsQueryHandler(
            IGameService gameService,
            IValidator validator)
        {
            _validator = validator;
            _gameService = gameService;
        }

        public async Task<IResult<IReadOnlyCollection<ScreenShotResponse>>> HandleAsync(GetScreenshotsQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<ScreenShotResponse>>();
            }

            var screenshots = await _gameService.GetScreenshots(query.GameId);

            return screenshots.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetScreenshotsQuery query)
        {
            return _validator
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .ValidateAsync();
        }
    }
}
