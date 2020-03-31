using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries;
using GamersHub.Api.Queries.Game;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers
{
    internal class GetHomeScreenGamesQueryHandler : IQueryHandler<GetHomeScreenGamesQuery, IReadOnlyCollection<GameWithImageResponse>>
    {
        private readonly DataContext _dataContext;
        private readonly IValidator _validator;

        public GetHomeScreenGamesQueryHandler(
            DataContext dataContext,
            IValidator validator)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<GameWithImageResponse>>> HandleAsync(GetHomeScreenGamesQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<GameWithImageResponse>>();
            }

            var games = query.HomeGamesCategory switch
            {
                HomeGamesCategory.ComingSoon => await _dataContext.Games.AsNoTracking()
                    .Skip(0)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.BrandNew => await _dataContext.Games.AsNoTracking()
                    .Skip(10)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.Hottest => await _dataContext.Games.AsNoTracking()
                    .Skip(20)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.OnSale => await _dataContext.Games.AsNoTracking()
                    .Skip(30)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                _ => new List<Game>()
            };

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

        private Task<IResult> IsValidAsync(GetHomeScreenGamesQuery query)
        {
            //TODO rethink what validation is needed here
            return _validator.ValidateAsync();
        }
    }
}
