﻿namespace GamersHub.Shared.Contracts.Requests.Base
{
    public abstract class BasePagingListRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
