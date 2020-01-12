﻿using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GamersHub.Api.Extensions
{
    public static class Extensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return httpContext.User == null
                ? Guid.Empty
                : Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
        }
    }
}
