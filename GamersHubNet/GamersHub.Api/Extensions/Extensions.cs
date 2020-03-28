using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Validation;

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

        public static bool HasFailed(this IResult result)
        {
            return !result.HasSucceeded;
        }

        public static void ValidateUserIds(this IValidator validator, Guid currentUserId, Guid? userId)
        {
            validator.Require<UserExistsRule>()
                .WithOptions(x => x.StopIfFailed())
                .WithData(currentUserId);

            if (userId.HasValue)
            {
                validator.Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(userId.Value);
            }
        }
    }
}
