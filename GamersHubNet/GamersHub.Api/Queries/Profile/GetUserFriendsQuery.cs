using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Profile
{
    public class GetUserFriendsQuery : IQuery<IReadOnlyCollection<UserProfileResponse>>
    {
        public Guid? UserId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
