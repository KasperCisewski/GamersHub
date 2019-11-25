using MvvmCross;

namespace GamerHub.mobile.core.Services.Dependency
{
    public class DependencyService : IDependencyService
    {
        public T Resolve<T>() where T : class
        {
            return Mvx.IoCProvider.Resolve<T>();
        }
    }
}
