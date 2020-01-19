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
using GamersHub.Api.Extensions;

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

            var screenShotModels = game.GameImages.Select(x => new ScreenShotModel { ImageContent = x.Data.ToList() });

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
            //    CoverImage = game.CoverGameImage.Data.ToList(),
            //    Description = game.Description,
            //    Price = x.Price,
            //    OfferUrl = x.OfferUrl,
            //    ShopName = x.Store.Name
            //});

            var priceModels = new List<PriceModel>
            {
                new PriceModel {CoverImage = game.CoverGameImage.Data.ToList(), Description = "Standard edition", OfferUrl = "https://www.greenmangaming.com/games/world-of-final-fantasy-pc/", Price = 102.50M, ShopName = "Green man gaming" },
                new PriceModel {CoverImage = game.CoverGameImage.Data.ToList(), Description = "Exclusive edition", OfferUrl = "https://www.greenmangaming.com/games/world-of-final-fantasy-pc/", Price = 12.50M, ShopName = "Green man gaming" },
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

            var games = homeGamesCategory switch
            {
                HomeGamesCategory.ComingSoon => await _dataContext.Games.AsNoTracking()
                    .Skip(0)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.BrandNew => await _dataContext.Games.AsNoTracking()
                    .Skip(10)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.Hottest => await _dataContext.Games.AsNoTracking()
                    .Skip(20)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                HomeGamesCategory.OnSale => await _dataContext.Games.AsNoTracking()
                    .Skip(30)
                    .Take(10)
                    .Include(x => x.CoverGameImage)
                    .ToListAsync(),
                _ => new List<Game>()
            };

            return games.Select(x => new GameModelWithImage
            {
                Id = x.Id,
                Category = x.GameCategory,
                Title = x.Name,
                ImageBytes = x.CoverGameImage.Data.ToList()
            });
        }

        [HttpGet(ApiRoutes.Games.GetFullGameDescription)]
        public async Task<IActionResult> GetFullDescriptionForGame(Guid gameId)
        {
            var game = await _dataContext.Games
                .AsNoTracking()
                .Include(x => x.CoverGameImage)
                .FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                return BadRequest("There is no game with given id");
            }

            var userId = HttpContext.GetUserId();
            var user = userId != Guid.Empty
                ? await _dataContext.Users
                    .Include(x => x.Games)
                    .Include(x => x.WishList)
                    .SingleOrDefaultAsync(x => x.Id == userId)
                : null;

            var model = new FullDescriptionGameModel
            {
                Description = game.Description,
                GeneralImage = game.CoverGameImage.Data.ToList(),
                Title = game.Name,
                ReleaseDate = game.ReleaseDate,
                UserHasGameInVault = user != null && user.Games.Any(x => x.GameId == gameId),
                UserHasGameOnWishList = user != null && user.WishList.Any(x => x.GameId == gameId)
            };

            return Ok(model);
        }
    }
}
