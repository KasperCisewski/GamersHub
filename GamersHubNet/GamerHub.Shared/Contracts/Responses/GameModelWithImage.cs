using GamersHub.Shared.Model;

namespace GamersHub.Shared.Contracts.Responses
{
    public class GameModelWithImage : GameModel
    {
        public byte[] ImageTitle { get; set; }
    }
}
