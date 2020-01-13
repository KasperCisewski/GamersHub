using System.Collections.Generic;
using Android.Graphics;

namespace GamerHub.mobile.core.Models
{
    public class GameScreenshotRowModel
    {
        public Bitmap ScreenshotBitmap { get; set; }

        public GameScreenshotRowModel(
            List<byte> imageContent)
        {
            ScreenshotBitmap = BitmapFactory.DecodeByteArray(imageContent.ToArray(), 0, imageContent.Count);
        }
    }
}
