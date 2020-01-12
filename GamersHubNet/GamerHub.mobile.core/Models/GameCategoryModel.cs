using GamerHub.mobile.core.Services.Resource;
using GamersHub.Shared.Data.Enums;

namespace GamerHub.mobile.core.Models
{
    public class GameCategoryModel
    {
        public int ImageId { get; set; }
        public GameCategory GameCategory { get; set; }

        public GameCategoryModel(
            GameCategory gameCategory,
            IResourceService resourceService
            )
        {
            GameCategory = gameCategory;
            var imageUrl = gameCategory.ToString();
            ImageId = resourceService.GetDrawableId(imageUrl);
        }
    }
}
