using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.CommandHandlers
{
    internal class AddGameToVaultCommandHandler : ICommandHandler<AddGameToVaultCommand>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public AddGameToVaultCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult> HandleAsync(AddGameToVaultCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            var game = await _dataContext.Games.FindAsync(command.GameId);
            var user = await _dataContext.Users.FindAsync(command.UserId);

            var vaultEntry = new UserGame()
            {
                Game = game,
                User = user,
            };

            user.Games.Add(vaultEntry);

            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(AddGameToVaultCommand query)
        {
            return _validator
                .Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.UserId)
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .Require<GameNotInVaultAlreadyRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData((query.GameId, query.UserId))
                .ValidateAsync();
        }
    }
}
