using GamerHub.mobile.core.Services;
using System;
using Android.Content;
using Android.Views.InputMethods;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace GamerHub.mobile.android.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideKeyboard()
        {
            try
            {
                var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

                if (context != null)
                {
                    var imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
                    imm?.HideSoftInputFromWindow(context.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
                }
            }
            catch (Exception)
            {
                // Ignore 
            }
        }
    }
}