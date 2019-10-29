using MvvmCross.Droid.Support.V7.AppCompat;
using Android.App;
using Android.OS;
using Android.Views;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.Login;
using Android.Graphics;
using Android.Content.Res;
using MvvmCross;
using MvvmCross.Platforms.Android;
using Calligraphy;
using Android.Content;

namespace GamerHub.mobile.android.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            
            CalligraphyConfig.InitDefault(
             new CalligraphyConfig.Builder()
               .SetDefaultFontPath("fonts/HACKED.ttf")
               .SetFontAttrId(Resource.Attribute.fontPath)
               .Build()
           );

            //var font = Typeface.CreateFromAsset(Assets, "HACKED.ttf");

            ////Typeface.Serif = font;

            ////Infragistics.Core.Controls.TypefaceManager.Instance.OverrideDefaultTypeface(font);

            //var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            //AssetManager am = context.Assets;

            //context.Assets.

            Window.SetSoftInputMode(SoftInput.AdjustResize | SoftInput.StateHidden);
            Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
            await ViewModel.ShowViewModel<LoginViewModel>();


        }

        protected override void AttachBaseContext(Context newBase)
        {
            base.AttachBaseContext(CalligraphyContextWrapper.Wrap(newBase));
        }

    }
}