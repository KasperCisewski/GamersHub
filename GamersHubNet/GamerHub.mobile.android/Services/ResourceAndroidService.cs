using Android.Content.Res;
using GamerHub.mobile.core.Services.Resource;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace GamerHub.mobile.android.Services
{
    public class ResourceAndroidService : IResourceService
    {
        public int GetDrawableId(string key)
        {
            var resourceId = (int)typeof(GamerHub.mobile.android.Resource.Drawable).GetField(key).GetValue(null);
            return resourceId;
        }

        public Resources GetResources()
        {
            return Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity.Resources;
        }
    }
}