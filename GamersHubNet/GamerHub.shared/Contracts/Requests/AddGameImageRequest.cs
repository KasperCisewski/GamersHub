using Microsoft.AspNetCore.Http;
using System;

namespace GamersHub.Shared.Contracts.Requests
{
    public class AddGameImageRequest
    {
        public Guid GameId { get; set; }
        public IFormFile Image { get; set; }
    }
}
