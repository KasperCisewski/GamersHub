using System.Collections.Generic;

namespace GamersHub.Shared.Contracts.Responses
{
    public class PriceModel
    {
        public List<byte> CoverImage { get; set; }
        public string OfferUrl { get; set; }
        public string Description { get; set; }
        public string ShopName { get; set; }
        public decimal Price { get; set; }
    }
}
