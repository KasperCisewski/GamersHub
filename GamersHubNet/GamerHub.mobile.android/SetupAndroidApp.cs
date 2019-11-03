using System;
using Android.App;
using Android.Runtime;

namespace GamerHub.mobile.android
{
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable=false)]
#endif
    public class SetupAndroidApplication : Application
    {
        public SetupAndroidApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }
    }
}