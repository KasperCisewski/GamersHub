using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries
{
    public class GetHomeScreenGamesQuery : IQuery<IReadOnlyCollection<GameModelWithImage>>
    {
        public HomeGamesCategory HomeGamesCategory { get; set; }
    }
}
