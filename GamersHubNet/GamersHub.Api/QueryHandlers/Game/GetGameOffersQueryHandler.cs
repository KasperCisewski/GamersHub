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
    internal class GetGameOffersQueryHandler : IQueryHandler<GetGameOffersQuery, IReadOnlyCollection<GameOfferResponse>>
    {
        private readonly DataContext _dataContext;
        private readonly IValidator _validator;

        public GetGameOffersQueryHandler(
            DataContext dataContext,
            IValidator validator)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<GameOfferResponse>>> HandleAsync(GetGameOffersQuery query)
        {
            var validationResult = await IsValidAsync(query);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<GameOfferResponse>>();
            }

            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.CoverGameImage)
                .Include(x => x.GameOffers)
                .FirstOrDefaultAsync(x => x.Id == query.GameId);

            // TODO implement fetching real offers
            var gameOffers = new List<GameOfferResponse>
            {
                new GameOfferResponse {CoverImage = game.CoverGameImage.Data.ToList(), Description = "Standard edition", OfferUrl = "https://www.greenmangaming.com/games/world-of-final-fantasy-pc/", Price = 102.50M, ShopName = "Green man gaming" },
                new GameOfferResponse {CoverImage = game.CoverGameImage.Data.ToList(), Description = "Exclusive edition", OfferUrl = "https://www.greenmangaming.com/games/world-of-final-fantasy-pc/", Price = 12.50M, ShopName = "Green man gaming" },
            };

            return gameOffers.ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetGameOffersQuery query)
        {
            return _validator
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .ValidateAsync();
        }
    }
}
