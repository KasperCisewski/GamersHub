using System;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using Gybs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.ValidationRules
{
    public class GameExistsRule : IValidationRule<Guid>
    {
        public const string GameDoesNotExist = "no-game-with-given-id";
        private readonly DataContext _dataContext;

        public GameExistsRule(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IResult> ValidateAsync(Guid gameId)
        {
            if (await _dataContext.Games.AnyAsync(x => x.Id == gameId))
            {
                return Result.Success();
            }

            return Result.Failure(GameDoesNotExist);
        }
    }
}