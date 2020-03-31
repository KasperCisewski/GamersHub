using System;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Game
{
    public class GetFullGameDescriptionQuery : IQuery<FullGameDescriptionResponse>
    {
        public Guid GameId { get; set; }
        public Guid? UserId { get; set; }
    }
}
