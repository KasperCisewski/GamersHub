using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries
{
    public class GetGameOffersQuery : IQuery<IReadOnlyCollection<GameOfferModel>>
    {
        public Guid GameId { get; set; }
    }
}
