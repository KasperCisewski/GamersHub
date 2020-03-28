using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.CommandHandlers
{
    internal class AddFriendCommandHandler : ICommandHandler<AddFriendCommand>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public AddFriendCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult> HandleAsync(AddFriendCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            var friendship = new Friendship { CurrentUserId = command.CurrentUserId, FriendId = command.UserId };
            var friendshipReversed = new Friendship { CurrentUserId = command.UserId, FriendId = command.CurrentUserId };

            _dataContext.Friendships.Add(friendship);
            _dataContext.Friendships.Add(friendshipReversed);

            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(AddFriendCommand query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
