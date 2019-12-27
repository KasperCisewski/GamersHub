using System.Collections.Generic;
using GamersHub.Shared.Model;

namespace GamersHub.Shared.Contracts.Responses
{
    public class GameModelWithImage : GameModel
    {
        public List<byte> ImageBytes { get; set; }
    }
}
