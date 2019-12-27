using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamersHub.Api.Controllers
{
    public class GameController : Controller
    {
        private readonly DataContext _dataContext;

        public GameController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet(ApiRoutes.Games.GetScreenshotsForGame)]
        public async Task<IActionResult> GetScreenshots(Guid gameId)
        {
            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.GameImages)
                .FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                return BadRequest("There is no game with given id");
            }

            var screenShotModels = game.GameImages.Select(x => new ScreenShotModel { ImageContent = x.Data });

            return Ok(screenShotModels);
        }

        [HttpGet(ApiRoutes.Games.GetPricesForGame)]
        public async Task<IActionResult> GetPrices(Guid gameId)
        {
            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.CoverGameImage)
                .Include(x => x.GameOffers)
                .FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                return BadRequest("There is no game with given id");
            }

            // TODO

            //var priceModels = game.GameOffers.Select(x => new PriceModel
            //{
            //    CoverImage = game.CoverGameImage.Data,
            //    Description = game.Description,
            //    Price = x.Price,
            //    OfferUrl = x.OfferUrl,
            //    ShopName = x.Store.Name
            //});

            var priceModels = new List<PriceModel>
            {
                new PriceModel(){CoverImage = game.CoverGameImage.Data, Description = game.Description, OfferUrl = "https://www.greenmangaming.com/games/world-of-final-fantasy-pc/", Price = 102.50M, ShopName = "Green man gaming" },
                new PriceModel(){CoverImage = game.CoverGameImage.Data, Description = game.Description, OfferUrl = "https://www.greenmangaming.com/games/world-of-final-fantasy-pc/", Price = 12.50M, ShopName = "Green man gaming" },
            };

            return Ok(priceModels);
        }
    }
}
