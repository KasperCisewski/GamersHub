using System;
using System.Collections.Generic;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Profile
{
    public class GetUserGamesNamesQuery : IQuery<IReadOnlyCollection<string>>
    {
        public Guid? UserId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
