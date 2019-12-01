using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamersHub.Api.Domain
{
    public class GameOffer
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
