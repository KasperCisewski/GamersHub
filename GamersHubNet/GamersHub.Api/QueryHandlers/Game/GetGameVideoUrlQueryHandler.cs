using System.Threading.Tasks;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Game;
using GamersHub.Api.Services;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.QueryHandlers.Game
{
    internal class GetGameVideoUrlQueryHandler : IQueryHandler<GetGameVideoUrlQuery, string>
    {
        private readonly IGameService _gameService;
        private readonly IValidator _validator;

        public GetGameVideoUrlQueryHandler(
            IGameService gameService,
            IValidator validator)
        {
            _validator = validator;
            _gameService = gameService;
        }

        public async Task<IResult<string>> HandleAsync(GetGameVideoUrlQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<string>();
            }

            var url = await _gameService.GetGameVideoUrl(query.GameId);

            return url.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetGameVideoUrlQuery query)
        {
            return _validator
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .ValidateAsync();
        }
    }
}
