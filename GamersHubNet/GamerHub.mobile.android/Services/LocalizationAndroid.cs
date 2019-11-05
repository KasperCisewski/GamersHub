using MvvmCross;
using MvvmCross.Platforms.Android;
using System;

namespace GamerHub.mobile.android.Services
{
    public class LocalizationAndroid
    {
        public string GetString(string key)
        {
            try
            {
                var currentTopActivity = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
                var activity = currentTopActivity.Activity;
                var field = typeof(Resource.String).GetField(key);
                var resourceId = (int)field.GetValue(null);
                return activity.GetString(resourceId);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}