using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Profile;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers.Profile
{
    internal class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, UserProfileResponse>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public GetUserProfileQueryHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<UserProfileResponse>> HandleAsync(GetUserProfileQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<UserProfileResponse>();
            }

            var userId = query.UserId ?? query.CurrentUserId;

            var user = await _dataContext.Users.FindAsync(userId);

            var isFriend = await _dataContext.Friendships
                .AnyAsync(x => x.CurrentUserId == query.CurrentUserId && x.FriendId == userId);

            return new UserProfileResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfileImageContent = user.ProfileImage?.ToList(),
                IsUserFriend = isFriend
            }.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetUserProfileQuery query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
