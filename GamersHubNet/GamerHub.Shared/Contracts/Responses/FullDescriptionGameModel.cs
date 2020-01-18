using System;
using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class FullDescriptionGameModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<byte> GeneralImage { get; set; }
        public bool IsUserHasGameInVault { get; set; }
        public bool IsUserHasGameInWishList { get; set; }
    }
}
