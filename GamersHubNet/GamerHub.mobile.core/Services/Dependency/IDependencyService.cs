namespace GamerHub.mobile.core.Services.Dependency
{
    public interface IDependencyService
    {
        T Resolve<T>() where T : class;
    }
}
