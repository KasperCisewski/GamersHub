using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.CommandHandlers
{
    public class ChangeProfileImageCommandHandler : ICommandHandler<ChangeProfileImageCommand>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public ChangeProfileImageCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult> HandleAsync(ChangeProfileImageCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            var user = await _dataContext.Users.FindAsync(command.CurrentUserId);
            user.ProfileImage = command.ImageContent;

            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(ChangeProfileImageCommand command)
        {
            return _validator
                .Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(command.CurrentUserId)
                .Require<ImageNotEmptyRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(command.ImageContent)
                .ValidateAsync();
        }
    }
}
