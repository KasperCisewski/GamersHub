using GamerHub.mobile.core.ViewModels.Base;
using MvvmCross.Droid.Support.V4;

namespace GamerHub.android.Views.Fragments.Base
{
    public class FragmentBase<T> : MvxFragment<T> where T : BaseViewModel
    {
    }
}