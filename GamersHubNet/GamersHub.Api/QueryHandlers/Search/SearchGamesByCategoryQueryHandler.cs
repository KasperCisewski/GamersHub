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
    internal class SearchGamesByCategoryQueryHandler : IQueryHandler<SearchGamesByCategoryQuery, IReadOnlyCollection<GameWithImageResponse>>
    {
        private readonly IValidator _validator;
        private readonly ISearchService _searchService;

        public SearchGamesByCategoryQueryHandler(
            IValidator validator,
            ISearchService searchService)
        {
            _validator = validator;
            _searchService = searchService;
        }

        public async Task<IResult<IReadOnlyCollection<GameWithImageResponse>>> HandleAsync(SearchGamesByCategoryQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<GameWithImageResponse>>();
            }

            return (await _searchService
                .SearchGamesByCategory(query.GameCategory, query.Skip, query.Take))
                .ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(SearchGamesByCategoryQuery query)
        {
            return _validator
                .Require<SkipTakeValuesValidRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData((query.Skip, query.Take))
                .ValidateAsync();
        }
    }
}
