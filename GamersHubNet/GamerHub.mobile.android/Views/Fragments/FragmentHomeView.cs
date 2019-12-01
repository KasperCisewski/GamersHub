using Android.OS;
using Android.Runtime;
using Android.Views;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Home;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentHomeView")]
    public class FragmentHomeView : FragmentBase<HomeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Home_View, null);

            return view;
        }
    }
}