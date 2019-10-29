using Microsoft.AspNetCore.Mvc;

namespace GamersHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { name = "marcin" });
        }
    }
}