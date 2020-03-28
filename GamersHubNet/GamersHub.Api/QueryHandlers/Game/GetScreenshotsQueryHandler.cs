using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries;
using GamersHub.Api.Queries.Game;
using GamersHub.Api.ValidationRules;
using GamersHub.Shared.Contracts.Responses;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers
{
    internal class GetScreenshotsQueryHandler : IQueryHandler<GetScreenshotsQuery, IReadOnlyCollection<ScreenShotResponse>>
    {
        private readonly DataContext _dataContext;
        private readonly IValidator _validator;

        public GetScreenshotsQueryHandler(
            DataContext dataContext,
            IValidator validator)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<ScreenShotResponse>>> HandleAsync(GetScreenshotsQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<ScreenShotResponse>>();
            }

            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.GameImages)
                .FirstOrDefaultAsync(x => x.Id == query.GameId);

            var screenShotModels = game.GameImages
                .Select(x => new ScreenShotResponse { ImageContent = x.Data.ToList()})
                .ToList();

            return screenShotModels.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetScreenshotsQuery query)
        {
            return _validator
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .ValidateAsync();
        }
    }
}
