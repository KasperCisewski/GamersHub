using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.CommandHandlers
{
    internal class DeleteFriendCommandHandler : ICommandHandler<DeleteFriendCommand>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public DeleteFriendCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult> HandleAsync(DeleteFriendCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            var friendship = await _dataContext.Friendships
                .SingleOrDefaultAsync(x => x.CurrentUserId == command.CurrentUserId && x.FriendId == command.UserId);
            var friendshipReversed = await _dataContext.Friendships
                .SingleOrDefaultAsync(x => x.CurrentUserId == command.UserId && x.FriendId == command.CurrentUserId);

            _dataContext.Friendships.Remove(friendship);
            _dataContext.Friendships.Remove(friendshipReversed);

            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(DeleteFriendCommand query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
