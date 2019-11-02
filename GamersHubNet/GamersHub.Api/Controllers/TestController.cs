using GamersHub.Api.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamersHub.Api.Controllers
{
    public class TestController : Controller
    {
        [HttpGet(ApiRoutes.Test.Get)]
        public IActionResult Get()
        {
            return Ok(new { name = "marcin" });
        }

        [HttpGet(ApiRoutes.Test.GetAuth)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AuthGet()
        {
            return Ok(new { name = "Tajne: marcin" });
        }
    }
}