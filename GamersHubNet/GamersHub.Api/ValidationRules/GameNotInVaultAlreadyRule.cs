using System;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using Gybs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.ValidationRules
{
    public class GameNotInVaultAlreadyRule : IValidationRule<(Guid gameId, Guid userId)>
    {
        public const string GameInVaultAlready = "no-game-with-given-id";

        private readonly DataContext _dataContext;

        public GameNotInVaultAlreadyRule(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IResult> ValidateAsync((Guid gameId, Guid userId) data)
        {
            var (gameId, userId) = data;
            var user = await _dataContext.Users
                .AsNoTracking()
                .Include(x => x.Games)
                .FirstAsync(x => x.Id == userId);

            if (user.Games.Any(x => x.GameId == gameId))
            {
                Result.Failure(GameInVaultAlready);
            }

            return Result.Success();
        }
    }
}
