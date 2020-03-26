using System;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries
{
    public class GetGameVideoUrlQuery : IQuery<string>
    {
        public Guid GameId { get; set; }
    }
}
