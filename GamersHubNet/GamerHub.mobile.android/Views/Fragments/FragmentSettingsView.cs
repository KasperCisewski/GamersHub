using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Settings;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentSettingsView")]
    public class FragmentSettingsView : FragmentBase<SettingsViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Settings_View, null);

            SetFontForView<TextView>(view, Resource.Id.app_info_text_view);
            SetFontForView<Button>(view, Resource.Id.take_profile_image_button_id);
            SetFontForView<Button>(view, Resource.Id.choose_profile_image_button_id);
            SetFontForView<Button>(view, Resource.Id.logout_button_id);

            return view;
        }
    }
}