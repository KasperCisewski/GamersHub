using Android.App;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android;

namespace GamerHub.android.Services
{
    public class ViewHistoryService : IViewHistoryService
    {
        public void ClearHistory()
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

            if (context is MvxAppCompatActivity compatContext)
            {
                compatContext.SupportFragmentManager.ExecutePendingTransactions();
                compatContext.SupportFragmentManager.PopBackStackImmediate(null, (int)PopBackStackFlags.Inclusive);
            }
        }
    }
}