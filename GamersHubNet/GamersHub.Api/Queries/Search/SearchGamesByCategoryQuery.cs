using System.Collections.Generic;
using GamersHub.Shared.Contracts.Requests.Base;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Search
{
    public class SearchGamesByCategoryQuery : BasePagingListRequest, IQuery<IReadOnlyCollection<GameWithImageResponse>>
    {
        public GameCategory GameCategory { get; set; }
    }
}