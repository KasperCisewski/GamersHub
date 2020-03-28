using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Game
{
    public class GetGameOffersQuery : IQuery<IReadOnlyCollection<GameOfferResponse>>
    {
        public Guid GameId { get; set; }
    }
}
