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
    internal class DeleteGameFromWishListCommandHandler : ICommandHandler<DeleteGameFromWishListCommand>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public DeleteGameFromWishListCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult> HandleAsync(DeleteGameFromWishListCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .FirstAsync(x => x.Id == command.UserId);

            var userGame = user.WishList.First(x => x.GameId == command.GameId);

            user.WishList.Remove(userGame);

            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(DeleteGameFromWishListCommand query)
        {
            return _validator
                .Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.UserId)
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .Require<UserHasGameOnWishListRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData((query.GameId, query.GameId))
                .ValidateAsync();
        }
    }
}
