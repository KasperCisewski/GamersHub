using System.Threading.Tasks;
using GamerHub.mobile.core.Models;
using GamerHub.mobile.core.Services.Game;
using GamerHub.mobile.core.ViewModels.Base;

namespace GamerHub.mobile.core.ViewModels.CoreApp.Game
{
    public partial class GamePricesViewModel : BaseViewModel<GameWithImageRowModel>
    {
        private readonly IGameService _gameService;

        public GamePricesViewModel(
            IGameService gameService)
        {
            _gameService = gameService;
        }
        public override void Prepare(GameWithImageRowModel parameter)
        {
            GameModel = parameter;
        }

        public override async Task Initialize()
        {
            var getPricesApiModelsForGame = await _gameService.GetPricesModelsForGame(GameModel.Id);

            foreach (var priceApiModel in getPricesApiModelsForGame)
            {
                GameOffers.Add(new GameOfferRowModel(priceApiModel.CoverImage, GameModel.Title, priceApiModel.Description, priceApiModel.Price, priceApiModel.ShopName, priceApiModel.OfferUrl));
            }
        }
    }
}
