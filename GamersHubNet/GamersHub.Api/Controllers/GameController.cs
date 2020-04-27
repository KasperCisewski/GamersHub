using GamersHub.Shared.Api;
using GamersHub.Shared.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Game;
using Gybs.Logic.Operations.Factory;

namespace GamersHub.Api.Controllers
{
    public class GameController : Controller
    {
        private readonly IOperationFactory _operationFactory;

        public GameController(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }

        [HttpGet(ApiRoutes.Games.GetGameScreenshots)]
        public async Task<IActionResult> GetScreenshots(Guid gameId)
        {
            var result = await _operationFactory.Create<GetScreenshotsQuery>(x =>
            {
                x.GameId =  gameId;
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Games.GetVideoUrl)]
        public async Task<IActionResult> GetGameVideoUrl(Guid gameId)
        {
            var result = await _operationFactory.Create<GetGameVideoUrlQuery>(x =>
            {
                x.GameId = gameId;
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Games.GetHomeScreenGames)]
        public async Task<IActionResult> GetHomeScreenGames(HomeGamesCategory homeGamesCategory)
        {
            var result = await _operationFactory.Create<GetHomeScreenGamesQuery>(x =>
            {
                x.HomeGamesCategory = homeGamesCategory;
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Games.GetFullGameDescription)]
        public async Task<IActionResult> GetFullDescriptionForGame(Guid gameId)
        {
            var result = await _operationFactory.Create<GetFullGameDescriptionQuery>(x =>
            {
                x.GameId = gameId;
                x.UserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
