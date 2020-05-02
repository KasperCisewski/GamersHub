using System.Collections.Generic;
using System.Threading.Tasks;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Search;
using GamersHub.Api.Services;
using GamersHub.Api.ValidationRules;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.QueryHandlers.Search
{
    internal class SearchUsersQueryHandler : IQueryHandler<SearchUsersQuery, IReadOnlyCollection<UserProfileResponse>>
    {
        private readonly IValidator _validator;
        private readonly ISearchService _searchService;

        public SearchUsersQueryHandler(
            IValidator validator,
            ISearchService searchService)
        {
            _validator = validator;
            _searchService = searchService;
        }

        public async Task<IResult<IReadOnlyCollection<UserProfileResponse>>> HandleAsync(SearchUsersQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<UserProfileResponse>>();
            }

            return (await _searchService
                    .SearchUsers(query.SearchText, query.CurrentUserId, query.Skip, query.Take))
            .ToSuccessfulResult();
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
