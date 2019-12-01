using GamerHub.mobile.core.Services.Resource;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace GamerHub.mobile.android.Services
{
    public class ResourceAndroidService : IResourceService
    {
        public int GetDrawableId(string key)
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var resourceId = (int)typeof(GamerHub.mobile.android.Resource.Drawable).GetField(key).GetValue(null);
            return resourceId;
        }
    }
}