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
    public class WishListController : Controller
    {
        private readonly DataContext _dataContext;

        public WishListController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost(ApiRoutes.Games.AddGameToWishList)]
        [Authorize]
        public async Task<IActionResult> AddGame([FromBody] AddGameVaultOrWishListRequest request)
        {
            var userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                .Include(x => x.WishList)
                .SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest("No user with given id found");
            }

            var game = await _dataContext.Games
                .SingleOrDefaultAsync(x => x.Id == request.GameId);

            if (game == null)
            {
                return BadRequest("No game with given id found");
            }

            if (user.WishList.Any(x => x.GameId == request.GameId))
            {
                return BadRequest("Game already on the wishlist");
            }

            var wishListEntry = new WishListEntry
            {
                Game = game,
                User = user,
            };

            user.WishList.Add(wishListEntry);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete(ApiRoutes.Games.DeleteGameFromWishList)]
        [Authorize]
        public async Task<IActionResult> DeleteGameFromVault(DeleteGameRequest request)
        {
            var userId = HttpContext.GetUserId();

            var user = await _dataContext.Users
                .Include(x => x.WishList)
                .SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest("No user with given id found");
            }

            if (user.WishList.All(x => x.GameId != request.GameId))
            {
                return BadRequest("User does not have this game in vault");
            }

            var userGame = user.WishList.SingleOrDefault(x => x.GameId == request.GameId);

            user.WishList.Remove(userGame);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}