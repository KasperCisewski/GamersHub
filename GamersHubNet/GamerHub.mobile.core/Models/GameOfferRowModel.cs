using System.Collections.Generic;
using Android.Graphics;

namespace GamerHub.mobile.core.Models
{
    public class GameOfferRowModel
    {
        public Bitmap GameCoverImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
        public string OfferUrl { get; set; }
        public GameOfferRowModel(
            List<byte> imageBytes,
            string title,
            string description,
            decimal price,
            string shopName,
            string offerUrl)
        {
            GameCoverImage = BitmapFactory.DecodeByteArray(imageBytes.ToArray(), 0, imageBytes.Count);
            Title = title;
            Description = description;
            Price = price;
            ShopName = shopName;
            OfferUrl = offerUrl;
        }
    }
}
