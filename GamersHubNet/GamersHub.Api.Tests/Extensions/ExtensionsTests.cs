using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluentAssertions;
using GamersHub.Api.Extensions;
using Gybs;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace GamersHub.Api.Tests.Extensions
{
    public class ExtensionsTests
    {
        private readonly Mock<HttpContext> _httpContext;

        public ExtensionsTests()
        {
            _httpContext = new Mock<HttpContext>();
        }

        [Fact]
        public void GetUserId_ForHttpContextUserNull_ShouldReturnGuidEmpty()
        {
            _httpContext.Setup(x => x.User).Returns((ClaimsPrincipal)null);

            var userId = _httpContext.Object.GetUserId();

            userId.Should().Be(Guid.Empty);
        }

        [Fact]
        public void GetUserId_ForHttpContextUserNotNull_ShouldReturnProperId()
        {
            var id = Guid.NewGuid();
            _httpContext.Setup(x => x.User).Returns(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim("id", id.ToString())
                })
            }));

            var userId = _httpContext.Object.GetUserId();

            userId.Should().Be(id);
        }

        [Fact]
        public void HasFailed_ForResultSucceeded_ShouldReturnFalse()
        {
            var result = new Mock<IResult>();
            result.Setup(x => x.HasSucceeded).Returns(true);

            var hasFailed = result.Object.HasFailed();

            hasFailed.Should().BeFalse();
        }

        [Fact]
        public void HasFailed_ForResultFailed_ShouldReturnTrue()
        {
            var result = new Mock<IResult>();
            result.Setup(x => x.HasSucceeded).Returns(false);

            var hasFailed = result.Object.HasFailed();

            hasFailed.Should().BeTrue();
        }
    }
}
