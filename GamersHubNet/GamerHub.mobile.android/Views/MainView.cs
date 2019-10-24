using Android.OS;
using Android.Views;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.Login;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace GamerHub.mobile.android.Views
{
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //ViewModel.SetDefaultLocale();
            //ViewModel.OnChangeOrientationAction = orientationName =>
            //{
            //    if (Enum.TryParse(orientationName, out ScreenOrientation screenOrientation))
            //    {
            //        RequestedOrientation = screenOrientation;
            //    }
            //};
            //ViewModel.ChangeOrientation(); ;

            SetContentView(Resource.Layout.Main_View);

            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            Window.SetSoftInputMode(SoftInput.AdjustResize | SoftInput.StateHidden);
            Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
            await ViewModel.ShowViewModel<LoginViewModel>();

            //ConfigureBinding();

            //_pausing = false;

            //ConfigureActivity();
            //await ConfigureFragment();
        }

    }
}