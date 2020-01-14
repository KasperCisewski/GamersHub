using System;
using System.Linq;
using Android.Graphics;
using GamersHub.Shared.Contracts.Responses;
using IResourceService = GamerHub.mobile.core.Services.Resource.IResourceService;

namespace GamerHub.mobile.core.Models
{
    public class UserProfileModel
    {
        public Bitmap ImageBitmap { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public UserProfileModel(
            UserProfile model,
            IResourceService resourceService)
        {
            //TODO
            if (model.ProfileImageContent != null && model.ProfileImageContent.Any())
            {
                ImageBitmap = BitmapFactory.DecodeByteArray(model.ProfileImageContent.ToArray(), 0, model.ProfileImageContent.Count);
            }
            else
            {
                var profileImageId = resourceService.GetDrawableId("ProfileImage");
                var resource = resourceService.GetResources();

                ImageBitmap = BitmapFactory.DecodeResource(resource, profileImageId);
            }
            Id = model.Id;
            UserName = model.UserName;
        }
    }
}
