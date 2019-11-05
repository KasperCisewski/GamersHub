using GamersHub.Api.Services;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GamersHub.Api.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet(ApiRoutes.Identity.UserWithEmailExists)]
        public bool UserWithEmailExists(string email) => _identityService.UserWithEmailExists(email);

        [HttpGet(ApiRoutes.Identity.UserWithUsernameExists)]
        public bool UserWithUsernameExists(string username) => _identityService.UserWithUsernameExists(username);

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailureResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password, request.Username);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailureResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse { Token = authResponse.Token });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailureResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse { Token = authResponse.Token });
        }
    }
}
