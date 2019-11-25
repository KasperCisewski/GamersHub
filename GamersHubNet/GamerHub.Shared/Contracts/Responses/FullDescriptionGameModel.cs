using System;

namespace GamersHub.Shared.Contracts.Responses
{
    public class FullDescriptionGameModel : GameModelWithImage
    {
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
