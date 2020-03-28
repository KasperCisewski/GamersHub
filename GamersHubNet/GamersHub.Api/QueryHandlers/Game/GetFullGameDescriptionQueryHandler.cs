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
    internal class GetFullGameDescriptionQueryHandler : IQueryHandler<GetFullGameDescriptionQuery, FullGameDescriptionResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IValidator _validator;

        public GetFullGameDescriptionQueryHandler(
            DataContext dataContext,
            IValidator validator)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<FullGameDescriptionResponse>> HandleAsync(GetFullGameDescriptionQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<FullGameDescriptionResponse>();
            }

            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.CoverGameImage)
                .FirstOrDefaultAsync(x => x.Id == query.GameId);

            var model = new FullGameDescriptionResponse
            {
                Description = game.Description,
                GeneralImage = game.CoverGameImage.Data.ToList(),
                Title = game.Name,
                ReleaseDate = game.ReleaseDate,
            };

            if (query.UserId != null)
            {
                var user = await _dataContext.Users
                    .Include(x => x.Games)
                    .Include(x => x.WishList)
                    .SingleOrDefaultAsync(x => x.Id == query.UserId);

                model.UserHasGameInVault = user.Games.Any(x => x.GameId == query.GameId);
                model.UserHasGameOnWishList = user.WishList.Any(x => x.GameId == query.GameId);
            }

            return model.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetFullGameDescriptionQuery query)
        {
            var validator = _validator
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId);

            if (query.UserId != null)
            {
                validator
                    .Require<UserExistsRule>()
                        .WithOptions(x => x.StopIfFailed())
                        .WithData(query.UserId.Value);
            }

            return validator.ValidateAsync();
        }
    }
}
