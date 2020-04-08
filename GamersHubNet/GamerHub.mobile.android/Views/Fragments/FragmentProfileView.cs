using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Profile;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentProfileView")]
    public class FragmentProfileView : FragmentBase<ProfileViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Profile_View, null);

            SetFontForView<TextView>(view, Resource.Id.profile_text_view);
            SetFontForView<TextView>(view, Resource.Id.profile_username_id);
            SetFontForView<Button>(view, Resource.Id.delete_friend_button);
            SetFontForView<Button>(view, Resource.Id.add_friend_button);
            SetFontsForSharedMenuBar(view);

            return view;
        }
    }
}