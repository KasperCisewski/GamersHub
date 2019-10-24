using Android.Runtime;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.Login;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("easystorage.mobile.Droid.Views.Fragments.PackSelectedContainerMainViewFragment")]
    public class FragmentLoginView : FragmentBase<LoginViewModel>
    {
    }
}