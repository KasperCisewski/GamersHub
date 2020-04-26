using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Extensions;
using GamersHub.Api.Services;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.CommandHandlers
{
    internal class DeleteFriendCommandHandler : ICommandHandler<DeleteFriendCommand>
    {
        private readonly IValidator _validator;
        private readonly IFriendService _friendService;

        public DeleteFriendCommandHandler(
            IValidator validator,
            IFriendService friendService)
        {
            _validator = validator;
            _friendService = friendService;
        }

        public async Task<IResult> HandleAsync(DeleteFriendCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            await _friendService.DeleteFriend(command.CurrentUserId, command.UserId);

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(DeleteFriendCommand query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
