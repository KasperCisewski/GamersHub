using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Profile;
using GamersHub.Api.Services;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.QueryHandlers.Profile
{
    public class GetUserFriendsQueryHandler : IQueryHandler<GetUserFriendsQuery, IReadOnlyCollection<UserProfileResponse>>
    {
        private readonly IValidator _validator;
        private readonly IFriendService _friendService;

        public GetUserFriendsQueryHandler(
            IValidator validator,
            IFriendService friendService)
        {
            _validator = validator;
            _friendService = friendService;
        }

        public async Task<IResult<IReadOnlyCollection<UserProfileResponse>>> HandleAsync(GetUserFriendsQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<UserProfileResponse>>();
            }

            var userId = query.UserId ?? query.CurrentUserId;
            var friends = await _friendService.GetFriends(userId);

            return friends.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetUserFriendsQuery query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
