using GamersHub.Shared.Contracts.Requests.Base;
using GamersHub.Shared.Data.Enums;

namespace GamersHub.Shared.Contracts.Requests
{
    public class GameCategoryRequest : BasePagingListRequest
    {
        public GameCategory GameCategory { get; set; }
    }
}
