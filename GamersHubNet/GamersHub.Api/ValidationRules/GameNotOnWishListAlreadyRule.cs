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
    public class GameNotOnWishListAlreadyRule : IValidationRule<(Guid gameId, Guid userId)>
    {
        public const string GameOnWishListAlready = "no-game-with-given-id";

        private readonly DataContext _dataContext;

        public GameNotOnWishListAlreadyRule(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IResult> ValidateAsync((Guid gameId, Guid userId) data)
        {
            var (gameId, userId) = data;
            var user = await _dataContext.Users
                .AsNoTracking()
                .Include(x => x.WishList)
                .FirstAsync(x => x.Id == userId);

            if (user.WishList.Any(x => x.GameId == gameId))
            {
                Result.Failure(GameOnWishListAlready);
            }

            return Result.Success();
        }
    }
}