using System;

namespace GamersHub.Shared.Contracts.Responses
{
    public class FullDescriptionGameModel
    {
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte[] GeneralImage { get; set; }
    }
}
