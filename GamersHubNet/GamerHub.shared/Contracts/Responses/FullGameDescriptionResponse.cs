using System;
using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class FullGameDescriptionResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<byte> GeneralImage { get; set; }
        public bool UserHasGameInVault { get; set; }
        public bool UserHasGameOnWishList { get; set; }
    }
}
