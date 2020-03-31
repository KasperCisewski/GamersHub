using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Profile;
using GamersHub.Shared.Data.Enums;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers.Profile
{
    public class GetUserGenresQueryHandler : IQueryHandler<GetUserGenresQuery, (Guid, IReadOnlyDictionary<string, int>)>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public GetUserGenresQueryHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<(Guid, IReadOnlyDictionary<string, int>)>> HandleAsync(GetUserGenresQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<(Guid, IReadOnlyDictionary<string, int>)>();
            }

            var userId = query.UserId ?? query.CurrentUserId;

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .SingleOrDefaultAsync(x => x.Id == userId);

            var userGames = user.Games.Select(x => x.GameId);

            var games = await _dataContext.Games
                .Where(x => userGames.Contains(x.Id))
                .ToListAsync();

            var countedGenres = games
                .GroupBy(x => x.GameCategory)
                .Select(g => new { GenreName = g.Key.ToString(), GenreCount = g.Count() })
                .ToDictionary(x => x.GenreName, x => x.GenreCount);

            Enum.GetNames(typeof(GameCategory))
                .ToImmutableList()
                .ForEach(x => countedGenres.TryAdd(x, 0));

            return (userId, (IReadOnlyDictionary<string, int>) countedGenres).ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetUserGenresQuery query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
