using System;
using System.Collections.Generic;
using GamersHub.Shared.Contracts.Requests.Base;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Search
{
    public class SearchUsersQuery : BasePagingListRequest, IQuery<IReadOnlyCollection<UserProfileResponse>>
    {
        public string SearchText { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}