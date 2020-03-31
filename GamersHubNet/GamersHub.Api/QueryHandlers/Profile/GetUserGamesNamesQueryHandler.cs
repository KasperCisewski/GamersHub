using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Profile;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers.Profile
{
    public class GetUserGamesNamesQueryHandler : IQueryHandler<GetUserGamesNamesQuery, IReadOnlyCollection<string>>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public GetUserGamesNamesQueryHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<string>>> HandleAsync(GetUserGamesNamesQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<string>>();
            }

            var userId = query.UserId ?? query.CurrentUserId;

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .ThenInclude(x => x.Game)
                .FirstAsync(x => x.Id == userId);

            return user.Games
                .Select(x => x.Game.Name)
                .ToList()
                .ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetUserGamesNamesQuery query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
