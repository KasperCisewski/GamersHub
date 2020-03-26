using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries
{
    public class GetScreenshotsQuery : IQuery<IReadOnlyCollection<ScreenShotModel>>
    {
        public Guid GameId { get; set; }
    }
}
