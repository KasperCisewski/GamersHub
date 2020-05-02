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
    public class UserHasGameOnWishListRule : IValidationRule<(Guid gameId, Guid userId)>
    {
        public const string UserDoesNotHaveGame = "user-does-not-have-game-on-wishlist";

        private readonly DataContext _dataContext;

        public UserHasGameOnWishListRule(DataContext dataContext)
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

            if (user.WishList.All(x => x.GameId != gameId))
            {
                Result.Failure(UserDoesNotHaveGame);
            }

            return Result.Success();
        }
    }
}