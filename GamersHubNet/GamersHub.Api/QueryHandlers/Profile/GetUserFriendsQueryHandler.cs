using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries;
using GamersHub.Api.Queries.Profile;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers.Profile
{
    public class GetUserFriendsQueryHandler : IQueryHandler<GetUserFriendsQuery, IReadOnlyCollection<UserProfileResponse>>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public GetUserFriendsQueryHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<UserProfileResponse>>> HandleAsync(GetUserFriendsQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<UserProfileResponse>>();
            }

            var userId = query.UserId ?? query.CurrentUserId;

            var friendsIds = _dataContext.Friendships
                .AsNoTracking()
                .Where(x => x.CurrentUserId == userId)
                .Select(x => x.FriendId);

            var friends = await _dataContext.Users
                .AsNoTracking()
                .Where(x => friendsIds.Contains(x.Id))
                .Select(x => new UserProfileResponse
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    IsUserFriend = true
                }).ToListAsync();

            return friends.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetUserFriendsQuery query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
