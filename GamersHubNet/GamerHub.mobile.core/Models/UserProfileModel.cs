using System;
using System.Linq;
using Android;
using Android.Content.Res;
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
            //TODO
            if (model.ProfileImageContent != null && model.ProfileImageContent.Any())
            {
                ImageBitmap = BitmapFactory.DecodeByteArray(model.ProfileImageContent.ToArray(), 0, model.ProfileImageContent.Count);
            }
            else
            {
                var profileImageId = (int)typeof(Resource.Drawable).GetField("ProfileImage.png").GetValue(null);
                ImageBitmap = BitmapFactory.DecodeResource(Resources.System, profileImageId);
            }
            Id = model.Id;
            UserName = model.UserName;
        }
    }
}
