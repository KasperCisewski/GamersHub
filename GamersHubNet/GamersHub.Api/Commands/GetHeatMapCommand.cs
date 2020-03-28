using System;
using System.Collections.Generic;
using Gybs.Logic.Cqrs;

namespace GamersHub.Api.Commands
{
    public class GetHeatMapCommand : ICommand<IReadOnlyCollection<byte>>
    {
        public Guid? UserId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
