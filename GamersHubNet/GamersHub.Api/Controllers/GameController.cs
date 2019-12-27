using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;
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

        [HttpGet(ApiRoutes.Games.GetVideoUrl)]
        public async Task<IActionResult> GetGameVideoUrl(Guid gameId)
        {
            var game = await _dataContext.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                return BadRequest("There is no game with given id");
            }

            return Ok(game.VideoUrl);
        }

        [HttpGet(ApiRoutes.Games.GetGamesForHomeScreen)]
        public async Task<IEnumerable<GameModelWithImage>> GetHomeScreenGames(HomeGamesCategory homeGamesCategory)
        {
            // TODO: better way of selecting games for home screen

            var games = new List<Game>();
            switch (homeGamesCategory)
            {
                case HomeGamesCategory.ComingSoon:
                    games = await _dataContext.Games
                        .AsNoTracking()
                        .Skip(0)
                        .Take(10)
                        .Include(x => x.CoverGameImage)
                        .ToListAsync();
                    break;
                case HomeGamesCategory.BrancNew:
                    games = await _dataContext.Games
                        .AsNoTracking()
                        .Skip(10)
                        .Take(10)
                        .Include(x => x.CoverGameImage)
                        .ToListAsync();
                    break;
                case HomeGamesCategory.Hottest:
                    games = await _dataContext.Games
                        .AsNoTracking()
                        .Skip(20)
                        .Take(10)
                        .Include(x => x.CoverGameImage)
                        .ToListAsync();
                    break;
                case HomeGamesCategory.OnSale:
                    games = await _dataContext.Games
                        .AsNoTracking()
                        .Skip(30)
                        .Take(10)
                        .Include(x => x.CoverGameImage)
                        .ToListAsync();
                    break;
                default:
                    break;
            }
            
            return games.Select(x => new GameModelWithImage
            {
                Id = x.Id,
                Category = x.GameCategory,
                Title = x.Name,
                ImageBytes = x.CoverGameImage.Data.ToList()
            });
        }
    }
}
