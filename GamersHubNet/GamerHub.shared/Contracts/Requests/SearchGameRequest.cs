using GamersHub.Shared.Contracts.Requests.Base;

namespace GamersHub.Shared.Contracts.Requests
{
    public class SearchGameRequest : BasePagingListRequest
    {
        public string SearchGameText { get; set; }
    }
}
