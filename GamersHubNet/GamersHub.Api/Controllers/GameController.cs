using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            return Ok(game.GameImages);
        }
    }
}
