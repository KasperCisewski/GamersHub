using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Search;
using GamersHub.Api.ValidationRules;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers.Search
{
    internal class SearchUsersQueryHandler : IQueryHandler<SearchUsersQuery, IReadOnlyCollection<UserProfileResponse>>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public SearchUsersQueryHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<UserProfileResponse>>> HandleAsync(SearchUsersQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<UserProfileResponse>>();
            }

            var users = await _dataContext.Users
                .AsNoTracking()
                .Where(x => x.UserName.Contains(query.SearchText))
                .Skip(query.Skip)
                .Take(query.Take == default ? 10 : query.Take)
                .Select(x => new UserProfileResponse
                {
                    Id = x.Id,
                    ProfileImageContent = null,
                    UserName = x.UserName,
                }).ToListAsync();

            foreach (var user in users)
            {
                user.IsUserFriend = await _dataContext.Friendships
                    .AnyAsync(x => x.CurrentUserId == query.CurrentUserId && x.FriendId == user.Id);
            }

            return users.ToList().ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(SearchUsersQuery query)
        {
            return _validator
                .Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.CurrentUserId)
                .Require<SearchTextLengthValidRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.SearchText)
                .Require<SkipTakeValuesValidRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData((query.Skip, query.Take))
                .ValidateAsync();
        }
    }
}
