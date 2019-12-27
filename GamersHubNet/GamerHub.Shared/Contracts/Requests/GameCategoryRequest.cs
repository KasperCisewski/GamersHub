using GamerHub.shared.Contracts.Requests.Base;
using GamerHub.shared.Contracts.Requests.Base;
using GamersHub.Shared.Data.Enums;

namespace GamersHub.Shared.Contracts.Requests
{
    public class GameCategoryRequest : BasePagingListRequest
    {
        public GameCategory GameCategory { get; set; }
    }
}
