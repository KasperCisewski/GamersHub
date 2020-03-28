using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Game
{
    public class GetHomeScreenGamesQuery : IQuery<IReadOnlyCollection<GameWithImageResponse>>
    {
        public HomeGamesCategory HomeGamesCategory { get; set; }
    }
}
