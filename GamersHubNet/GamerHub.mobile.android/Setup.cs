using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using GamerHub.mobile.android.Services;
using GamerHub.mobile.core.Services;
using GamerHub.mobile.core.Infrastructure;

namespace GamerHub.mobile.android
{
    public class Setup : MvxAppCompatSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton<IViewHistoryService>(() => new ViewHistoryService());
            Mvx.IoCProvider.RegisterType<IKeyboardService, KeyboardService>();
            Mvx.IoCProvider.RegisterType<IGlobalStateService, GlobalStateService>();
            Mvx.IoCProvider.RegisterSingleton<ILocalizationService>(new LocalizationAndroid());


            base.InitializeFirstChance();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions { PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject };
        }
    }
}