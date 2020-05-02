using GamersHub.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Extensions;
using GamersHub.Shared.Contracts.Requests;
using Gybs.Logic.Operations.Factory;
using Microsoft.AspNetCore.Authorization;

namespace GamersHub.Api.Controllers
{
    public class WishListController : Controller
    {
        private readonly IOperationFactory _operationFactory;

        public WishListController(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }

        [HttpPost(ApiRoutes.Games.AddGameToWishList)]
        [Authorize]
        public async Task<IActionResult> AddGame([FromBody] AddGameVaultOrWishListRequest request)
        {
            var result = await _operationFactory.Create<AddGameToWishListCommand>(x =>
            {
                x.GameId = request.GameId;
                x.UserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Games.DeleteGameFromWishList)]
        [Authorize]
        public async Task<IActionResult> DeleteGameFromWishList([FromBody] DeleteGameRequest request)
        {
            var result = await _operationFactory.Create<DeleteGameFromWishListCommand>(x =>
            {
                x.GameId = request.GameId;
                x.UserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}