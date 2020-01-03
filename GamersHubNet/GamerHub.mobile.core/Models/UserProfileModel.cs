using System;
using Android.Graphics;
using GamersHub.Shared.Contracts.Responses;

namespace GamerHub.mobile.core.Models
{
    public class UserProfileModel
    {
        public Bitmap ImageBitmap { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public UserProfileModel(
            UserProfile model)
        {
            ImageBitmap = BitmapFactory.DecodeByteArray(model.ProfileImageContent.ToArray(), 0, model.ProfileImageContent.Count);
            Id = model.Id;
            UserName = model.UserName;
        }
    }
}
