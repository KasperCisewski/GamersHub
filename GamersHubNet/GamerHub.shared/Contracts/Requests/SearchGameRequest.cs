using GamersHub.Shared.Contracts.Requests.Base;

namespace GamerHub.shared.Contracts.Requests
{
    public class SearchGameRequest : BasePagingListRequest
    {
        public string SearchGameText { get; set; }
    }
}
