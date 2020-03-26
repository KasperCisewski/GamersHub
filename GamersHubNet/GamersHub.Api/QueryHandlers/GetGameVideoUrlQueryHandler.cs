using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.QueryHandlers
{
    internal class GetGameVideoUrlQueryHandler : IQueryHandler<GetGameVideoUrlQuery, string>
    {
        private readonly DataContext _dataContext;
        private readonly IValidator _validator;

        public GetGameVideoUrlQueryHandler(
            DataContext dataContext,
            IValidator validator)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<string>> HandleAsync(GetGameVideoUrlQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<string>();
            }

            var game = await _dataContext.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == query.GameId);

            return game.VideoUrl.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetGameVideoUrlQuery query)
        {
            return _validator
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .ValidateAsync();
        }
    }
}
