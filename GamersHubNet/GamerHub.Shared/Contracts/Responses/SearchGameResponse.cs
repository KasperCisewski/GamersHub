using GamerHub.shared.Contracts.Requests;

namespace GamerHub.shared.Contracts.Responses
{
    public class SearchGameResponse : GameModel
    {
        public byte[] ImageTitle { get; set; }
    }
}
