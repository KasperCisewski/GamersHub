using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class GameWithImageResponse : GameResponse
    {
        public List<byte> ImageBytes { get; set; }
    }
}
