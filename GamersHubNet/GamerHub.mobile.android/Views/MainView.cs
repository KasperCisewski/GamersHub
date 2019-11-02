using MvvmCross.Droid.Support.V7.AppCompat;
using Android.App;
using Android.OS;
using Android.Views;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.Login;

namespace GamerHub.mobile.android.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            Window.SetSoftInputMode(SoftInput.AdjustResize | SoftInput.StateHidden);
            Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            await ViewModel.ShowViewModel<LoginViewModel>();
        }
    }
}