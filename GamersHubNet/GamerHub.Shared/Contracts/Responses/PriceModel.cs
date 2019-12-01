namespace GamersHub.Shared.Contracts.Responses
{
    public class PriceModel
    {
        public byte[] CoverImage { get; set; }
        public string ShopUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
