using System.Collections.Generic;
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
    internal class GetVaultGamesQueryHandler : IQueryHandler<GetVaultGamesQuery, IReadOnlyCollection<GameWithImageResponse>>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public GetVaultGamesQueryHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<GameWithImageResponse>>> HandleAsync(GetVaultGamesQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<GameWithImageResponse>> ();
            }

            var userId = query.UserId ?? query.CurrentUserId;

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .FirstAsync(x => x.Id == userId);

            var userGames = user.Games.Select(x => x.GameId);

            var games = await _dataContext.Games
                .Include(x => x.CoverGameImage)
                .Where(x => userGames.Contains(x.Id))
                .ToListAsync();

            return games
                .Select(x => new GameWithImageResponse
                {
                    Category = x.GameCategory,
                    Id = x.Id,
                    ImageBytes = x.CoverGameImage.Data.ToList(),
                    Title = x.Name
                })
                .ToList()
                .ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetVaultGamesQuery query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
