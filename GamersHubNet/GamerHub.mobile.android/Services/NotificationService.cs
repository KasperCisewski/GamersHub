using System.Timers;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.core.Services;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace GamerHub.mobile.android.Services
{
    public class NotificationService : INotificationService
    {
        private int _secondsLeft = 0;

        public void Notify(string text)
        {
            Notify(text, ToastLength.Short);
        }

        public void NotifyLong(string text)
        {
            Notify(text, ToastLength.Long);
        }

        public void NotifyVeryLong(string text)
        {
            Notify(text, 8);
        }

        private void Notify(string text, ToastLength length)
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            context.RunOnUiThread(() =>
            {
                var toast = Toast.MakeText(context, text, length);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            });
        }

        private void Notify(string text, int length)
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

            context.RunOnUiThread(() =>
            {
                var toast = Toast.MakeText(context, text, ToastLength.Short);

                toast.Show();

                _secondsLeft = length;

                var timer = new Timer { Interval = 1000, };

                timer.Elapsed += (sender, e) =>
                {
                    toast.Show();
                    toast.SetGravity(GravityFlags.Center, 0, 0);

                    _secondsLeft--;

                    if (_secondsLeft == 0)
                    {
                        timer.Stop();
                    }
                };

                timer.Start();
            });
        }
    }
}