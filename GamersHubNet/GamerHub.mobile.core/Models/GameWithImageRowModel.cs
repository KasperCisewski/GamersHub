using System;
using Android.Graphics;
using GamersHub.Shared.Contracts.Responses;
using GamersHub.Shared.Data.Enums;

namespace GamerHub.mobile.core.Models
{
    public class GameWithImageRowModel
    {
        public Bitmap ImageBitmap { get; set; }
        public Guid Id { get; set; }
        public GameCategory Category { get; set; }
        public string Title { get; set; }
        public GameWithImageRowModel(
            GameWithImageResponse withImageResponse)
        {
            ImageBitmap = BitmapFactory.DecodeByteArray(withImageResponse.ImageBytes.ToArray(), 0, withImageResponse.ImageBytes.Count);
            Id = withImageResponse.Id;
            Category = withImageResponse.Category;
            Title = withImageResponse.Title;
        }
    }
}
