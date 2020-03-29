using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.CommandHandlers
{
    internal class DeleteGameFromVaultCommandHandler : ICommandHandler<DeleteGameFromVaultCommand>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public DeleteGameFromVaultCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult> HandleAsync(DeleteGameFromVaultCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .FirstAsync(x => x.Id == command.UserId);

            var userGame = user.Games.First(x => x.GameId == command.GameId);

            user.Games.Remove(userGame);

            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(DeleteGameFromVaultCommand query)
        {
            return _validator
                .Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.UserId)
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .Require<UserHasGameRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData((query.GameId, query.GameId))
                .ValidateAsync();
        }
    }
}
