using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Game
{
    public class GetScreenshotsQuery : IQuery<IReadOnlyCollection<ScreenShotResponse>>
    {
        public Guid GameId { get; set; }
    }
}
