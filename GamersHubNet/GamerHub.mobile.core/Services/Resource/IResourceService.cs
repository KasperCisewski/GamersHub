using Android.Content.Res;

namespace GamerHub.mobile.core.Services.Resource
{
    public interface IResourceService
    {
        int GetDrawableId(string key);
        Resources GetResources();
    }
}
