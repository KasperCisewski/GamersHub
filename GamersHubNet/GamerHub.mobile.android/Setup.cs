using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using GamerHub.mobile.android.Services;
using GamerHub.mobile.core.Services;
using GamerHub.mobile.core.Infrastructure;
using GamerHub.mobile.core.Services.Http;
using GamerHub.mobile.core.Services.Http.Factory;
using GamerHub.mobile.core.Services.Resource;
using MvvmCross.Plugin.Messenger;

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
            Mvx.IoCProvider.RegisterType<INotificationService>(() => new NotificationService());
            Mvx.IoCProvider.RegisterSingleton<IViewHistoryService>(() => new ViewHistoryService());
            Mvx.IoCProvider.RegisterType<IKeyboardService, KeyboardService>();
            Mvx.IoCProvider.RegisterType<IHttpClientFactoryService>(() => new HttpClientFactoryService(Mvx.IoCProvider.Resolve<IPollyPolicyService>(), Mvx.IoCProvider.Resolve<IGlobalStateService>()));
            Mvx.IoCProvider.RegisterSingleton<IGlobalStateService>(() => new GlobalStateService(new MvxMessengerHub()));
            Mvx.IoCProvider.RegisterSingleton<ILocalizationService>(new LocalizationAndroid());
            Mvx.IoCProvider.RegisterSingleton<IResourceService>(new ResourceAndroidService());

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