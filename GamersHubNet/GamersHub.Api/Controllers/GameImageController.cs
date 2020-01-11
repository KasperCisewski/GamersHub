using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GamersHub.Api.Controllers
{
    public class GameImageController : Controller
    {
        private readonly DataContext _dataContext;

        public GameImageController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost(ApiRoutes.GameImages.AddGameImage)]
        [Authorize]
        public async Task<IActionResult> AddGameImage(AddGameImageRequest request)
        {
            if (request.Image == null || request.Image.Length == 0)
            {
                return BadRequest("No image sent");
            }

            var game = await _dataContext.Games
                .Include(x => x.GameImages)
                .FirstOrDefaultAsync(x => x.Id == request.GameId);
            
            if (game == null)
            {
                return BadRequest("No game found with given game id");
            }

            var gameImage = new GameImage
            {
                FileName = request.Image.FileName,
                Length = request.Image.Length,
                ContentType = request.Image.ContentType
            };

            await using (var ms = new MemoryStream())
            {
                await request.Image.CopyToAsync(ms);
                gameImage.Data = ms.ToArray();
            }

            _dataContext.Add(gameImage);
            game.GameImages.Add(gameImage);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
