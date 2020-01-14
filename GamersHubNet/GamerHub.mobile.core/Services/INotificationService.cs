namespace GamerHub.mobile.core.Services
{
    public interface INotificationService
    {
        void Notify(string text);
        void NotifyLong(string text);
        void NotifyVeryLong(string text);
    }
}
