using System;
using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Extensions;
using GamersHub.Api.Queries.Profile;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Requests;
using Gybs.Logic.Operations.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamersHub.Api.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IOperationFactory _operationFactory;

        public ProfileController(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }

        [HttpGet(ApiRoutes.Profile.ProfileRoot)]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(Guid? userId)
        {
            var result = await _operationFactory.Create<GetUserProfileQuery>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        [HttpGet(ApiRoutes.Profile.GetUserFriends)]
        [Authorize]
        public async Task<IActionResult> GetUserFriends(Guid? userId)
        {
            var result = await _operationFactory.Create<GetUserFriendsQuery>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(ApiRoutes.Profile.GetGamesInVault)]
        [Authorize]
        public async Task<IActionResult> GetVaultGames(Guid? userId)
        {
            var result = await _operationFactory.Create<GetVaultGamesQuery>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(ApiRoutes.Profile.GetWishListGames)]
        [Authorize]
        public async Task<IActionResult> GetGamesOnWishList(Guid? userId)
        {
            var result = await _operationFactory.Create<GetWishListGamesQuery>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(ApiRoutes.Profile.GetUserGenres)]
        public async Task<IActionResult> GetUserGenres(Guid? userId)
        {
            var result = await _operationFactory.Create<GetUserGenresQuery>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = userId.GetValueOrDefault();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Json(new { userId = result.Data.Item1, genres = result.Data.Item2 });
        }

        [HttpGet(ApiRoutes.Profile.GetUserGamesNames)]
        public async Task<IActionResult> GetUserGamesNames(Guid? userId)
        {
            var result = await _operationFactory.Create<GetUserGamesNamesQuery>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = userId.GetValueOrDefault();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Json(new { games = result.Data });
        }

        [HttpPost(ApiRoutes.Profile.UserFriendsRoot)]
        [Authorize]
        public async Task<IActionResult> AddFriend([FromBody] AddDeleteFriendRequest request)
        {
            var result = await _operationFactory.Create<AddFriendCommand>(x =>
            {
                x.UserId = request.UserId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Profile.UserFriendsRoot)]
        [Authorize]
        public async Task<IActionResult> DeleteFromFriendList([FromBody] AddDeleteFriendRequest request)
        {
            var result = await _operationFactory.Create<DeleteFriendCommand>(x =>
            {
                x.UserId = request.UserId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet(ApiRoutes.Profile.GetHeatMap)]
        [Authorize]
        public async Task<IActionResult> GetHeatMap(Guid? userId)
        {
            var result = await _operationFactory.Create<GetHeatMapCommand>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(ApiRoutes.Profile.GetRecommendedGames)]
        [Authorize]
        public async Task<IActionResult> GetRecommendedGames(Guid? userId)
        {
            var result = await _operationFactory.Create<GetRecommendedGamesCommand>(x =>
            {
                x.UserId = userId;
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPost(ApiRoutes.Profile.ChangeProfileImage)]
        [Authorize]
        public async Task<IActionResult> ChangeProfileImage([FromBody] ChangeProfileImageRequest imageEncoded)
        {
            var result = await _operationFactory.Create<ChangeProfileImageCommand>(x =>
            {
                x.ImageContent = Convert.FromBase64String(imageEncoded.ImageEncoded);
                x.CurrentUserId = HttpContext.GetUserId();
            }).HandleAsync();

            if (result.HasFailed())
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}