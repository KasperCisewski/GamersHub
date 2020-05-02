using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.Services;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.CommandHandlers
{
    internal class AddGameToWishListCommandHandler : ICommandHandler<AddGameToWishListCommand>
    {
        private readonly IValidator _validator;
        private readonly IGameService _gameService;

        public AddGameToWishListCommandHandler(
            IValidator validator,
            IGameService gameService)
        {
            _validator = validator;
            _gameService = gameService;
        }

        public async Task<IResult> HandleAsync(AddGameToWishListCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            await _gameService.AddGameToWishList(command.GameId, command.UserId);

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(AddGameToWishListCommand query)
        {
            return _validator
                .Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.UserId)
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .Require<GameNotOnWishListAlreadyRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData((query.GameId, query.UserId))
                .ValidateAsync();
        }
    }
}
