using System;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries
{
    public class GetFullGameDescriptionQuery : IQuery<FullDescriptionGameModel>
    {
        public Guid GameId { get; set; }
        public Guid? UserId { get; set; }
    }
}
