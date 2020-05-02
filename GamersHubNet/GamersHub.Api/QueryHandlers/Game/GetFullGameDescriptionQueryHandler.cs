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
    internal class GetFullGameDescriptionQueryHandler : IQueryHandler<GetFullGameDescriptionQuery, FullGameDescriptionResponse>
    {
        private readonly IValidator _validator;
        private readonly IGameService _gameService;

        public GetFullGameDescriptionQueryHandler(
            IGameService gameService,
            IValidator validator)
        {
            _validator = validator;
            _gameService = gameService;
        }

        public async Task<IResult<FullGameDescriptionResponse>> HandleAsync(GetFullGameDescriptionQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<FullGameDescriptionResponse>();
            }

            var model = await _gameService.GetFullGameDescription(query.GameId, query.UserId.GetValueOrDefault());

            return model.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetFullGameDescriptionQuery query)
        {
            var validator = _validator
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId);

            if (query.UserId != null)
            {
                validator
                    .Require<UserExistsRule>()
                        .WithOptions(x => x.StopIfFailed())
                        .WithData(query.UserId.Value);
            }

            return validator.ValidateAsync();
        }
    }
}
