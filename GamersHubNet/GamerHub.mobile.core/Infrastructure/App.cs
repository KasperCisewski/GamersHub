using MvvmCross.IoC;
namespace GamerHub.mobile.core.Infrastructure
{
    public class App : MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();
            CreatableTypes().EndingWith("Validator").AsInterfaces().RegisterAsLazySingleton();
            CreatableTypes().EndingWith("ViewModel").AsTypes().RegisterAsDynamic();
        }
    }
}
