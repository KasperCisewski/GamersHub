using GamersHub.Shared.Contracts.Requests.Base;

namespace GamersHub.Shared.Contracts.Requests
{
    public class SearchFriendsRequest : BasePagingListRequest
    {
        public string SearchUserNameText { get; set; }
    }
}
