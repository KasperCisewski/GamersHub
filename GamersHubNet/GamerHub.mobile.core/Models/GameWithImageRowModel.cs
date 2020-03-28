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
            GameModelWithImage model)
        {
            ImageBitmap = BitmapFactory.DecodeByteArray(model.ImageBytes.ToArray(), 0, model.ImageBytes.Count);
            Id = model.Id;
            Category = model.Category;
            Title = model.Title;
        }

        public GameWithImageRowModel(
            GameWithImageRowModel model,
            int highestWidth,
            int highestHeight)
        {
            ImageBitmap = Bitmap.CreateScaledBitmap(model.ImageBitmap, highestWidth, highestHeight, false);
            Id = model.Id;
            Category = model.Category;
            Title = model.Title;
        }
    }
}
