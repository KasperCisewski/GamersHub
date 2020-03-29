using System.Collections.Generic;
using GamersHub.Shared.Contracts.Requests.Base;
using GamersHub.Shared.Contracts.Responses;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Queries.Search
{
    public class SearchGamesQuery : BasePagingListRequest, IQuery<IReadOnlyCollection<GameWithImageResponse>>
    {
        public string SearchText { get; set; }
    }
}
