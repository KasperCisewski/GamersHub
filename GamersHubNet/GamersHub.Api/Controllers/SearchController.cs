using GamerHub.shared.Contracts.Requests;
using GamersHub.Api.Data;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamersHub.Api.Controllers
{
    public class SearchController : Controller
    {
        private readonly DataContext _dataContext;

        public SearchController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet(ApiRoutes.Search.SearchGames)]
        public async Task<IEnumerable<GameModelWithImage>> SearchGames([FromQuery] SearchGameRequest searchGameRequest)
        {
            var games = await _dataContext.Games
                .AsNoTracking()
                .Where(x => x.Name
                    .Contains(searchGameRequest.SearchGameText))
                .Include(x => x.CoverGameImage)
                .Skip(searchGameRequest.Skip)
                .Take(searchGameRequest.Take == default ? 10 : searchGameRequest.Take)
                .ToArrayAsync();

            return games.Select(x => new GameModelWithImage
            {
                Id = x.Id,
                Category = x.GameCategory,
                Title = x.Name,
                ImageTitle = x.CoverGameImage.Data
            });
        }
    }
}
