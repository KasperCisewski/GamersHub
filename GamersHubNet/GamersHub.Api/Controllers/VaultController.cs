using System;
using System.Linq;
using GamersHub.Api.Data;
using GamersHub.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Shared.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.Controllers
{
    public class VaultController : Controller
    {
        private readonly DataContext _dataContext;

        public VaultController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost(ApiRoutes.Games.AddGameToVault)]
        [Authorize]
        public async Task<IActionResult> AddGame([FromBody] AddGameVaultOrWishListRequest request)
        {
            var userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest("No user with given id found");
            }

            var game = await _dataContext.Games
                .FindAsync(request.GameId);

            if (game == null)
            {
                return BadRequest("No game with given id found");
            }

            if (user.Games.Any(x => x.GameId == request.GameId))
            {
                return BadRequest("Game already in the vault");
            }

            var vaultEntry = new UserGame()
            {
                Game = game,
                User = user,
            };

            user.Games.Add(vaultEntry);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete(ApiRoutes.Games.DeleteGameFromVault)]
        [Authorize]
        public async Task<IActionResult> DeleteGameFromVault(DeleteGameRequest request)
        {
            var userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                .Include(x => x.Games)
                .SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest("No user with given id found");
            }

            if (user.Games.All(x => x.GameId != request.GameId))
            {
                return BadRequest("User does not have this game in vault");
            }

            var userGame = user.Games.SingleOrDefault(x => x.GameId == request.GameId);

            user.Games.Remove(userGame);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}