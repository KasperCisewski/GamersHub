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
    internal class SearchGamesByCategoryQueryHandler : IQueryHandler<SearchGamesByCategoryQuery, IReadOnlyCollection<GameWithImageResponse>>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public SearchGamesByCategoryQueryHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<GameWithImageResponse>>> HandleAsync(SearchGamesByCategoryQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<GameWithImageResponse>>();
            }

            var games = await _dataContext.Games
                .AsNoTracking()
                .Where(x => x.GameCategory == query.GameCategory)
                .Include(x => x.CoverGameImage)
                .Skip(query.Skip)
                .Take(query.Take == default ? 10 : query.Take)
                .ToArrayAsync();

            return games
                .Select(x => new GameWithImageResponse
                {
                    Id = x.Id,
                    Category = x.GameCategory,
                    Title = x.Name,
                    ImageBytes = x.CoverGameImage.Data.ToList()
                })
                .ToList()
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
