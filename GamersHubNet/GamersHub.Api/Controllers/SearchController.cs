using GamersHub.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Search;
using GamersHub.Shared.Contracts.Requests;
using Gybs.Logic.Operations.Factory;

namespace GamersHub.Api.Controllers
{
    public class SearchController : Controller
    {
        private readonly IOperationFactory _operationFactory;

        public SearchController(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }

        [HttpGet(ApiRoutes.Search.SearchGames)]
        public async Task<IActionResult> SearchGames([FromQuery] SearchGameRequest searchGameRequest)
        {
            var result = await _operationFactory.Create<SearchGamesQuery>(x =>
            {
                x.SearchText = searchGameRequest.SearchGameText;
                x.Skip = searchGameRequest.Skip;
                x.Take = searchGameRequest.Take;
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet(ApiRoutes.Games.GetGamesByCategory)]
        public async Task<IActionResult> GetGamesByCategory([FromQuery] GameCategoryRequest gameCategoryRequest)
        {
            var result = await _operationFactory.Create<SearchGamesByCategoryQuery>(x =>
            {
                x.GameCategory = gameCategoryRequest.GameCategory;
                x.Skip = gameCategoryRequest.Skip;
                x.Take = gameCategoryRequest.Take;
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Search.SearchUsers)]
        public async Task<IActionResult> SearchUsers([FromQuery] SearchUserRequest searchUserRequest)
        {
            var result = await _operationFactory.Create<SearchUsersQuery>(x =>
            {
                x.SearchText = searchUserRequest.SearchUserNameText;
                x.CurrentUserId = HttpContext.GetUserId();
                x.Skip = searchUserRequest.Skip;
                x.Take = searchUserRequest.Take;
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
