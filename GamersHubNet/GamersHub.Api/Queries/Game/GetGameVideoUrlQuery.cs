using System;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Game
{
    public class GetGameVideoUrlQuery : IQuery<string>
    {
        public Guid GameId { get; set; }
    }
}
