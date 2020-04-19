using MvvmCross.Plugin.Messenger;

namespace GamerHub.mobile.core.Models.Messenger
{
    public class ProgressBarActivator : MvxMessage
    {
        public bool IsActive { get; set; }
        public ProgressBarActivator(object sender, bool isActive) : base(sender)
        {
            IsActive = isActive;
        }
    }
}
