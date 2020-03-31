using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Profile
{
    public class GetVaultGamesQuery : IQuery<IReadOnlyCollection<GameWithImageResponse>>
    {
        public Guid? UserId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
