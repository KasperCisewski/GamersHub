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
    public class VaultController : Controller
    {
        private readonly IOperationFactory _operationFactory;

        public VaultController(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }

        [HttpPost(ApiRoutes.Games.AddGameToVault)]
        [Authorize]
        public async Task<IActionResult> AddGame([FromBody] AddGameVaultOrWishListRequest request)
        {
            var result = await _operationFactory.Create<AddGameToVaultCommand>(x =>
            {
                x.GameId = request.GameId;
                x.UserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Games.DeleteGameFromVault)]
        [Authorize]
        public async Task<IActionResult> DeleteGameFromVault([FromBody] DeleteGameRequest request)
        {
            var result = await _operationFactory.Create<DeleteGameFromVaultCommand>(x =>
            {
                x.GameId = request.GameId;
                x.UserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}