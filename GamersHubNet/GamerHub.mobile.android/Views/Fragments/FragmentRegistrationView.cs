using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.Registration;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentRegistrationView")]
    public class FragmentRegistrationView : FragmentBase<RegistrationViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Registration_View, null);

            SetFontForView<TextInputEditText>(view, Resource.Id.registration_email_input_id);
            SetFontForView<TextInputEditText>(view, Resource.Id.registration_email_input_id);
            SetFontForView<TextInputEditText>(view, Resource.Id.registration_email_input_id);
            SetFontForView<TextInputEditText>(view, Resource.Id.registration_email_input_id);
            SetFontForView<TextView>(view, Resource.Id.registration_error_text_view);
            //SetFontForView<TextView>(view, Resource.Id.registration_email_input_id);
            //SetFontForView<TextView>(view, Resource.Id.registration_email_input_id);
            //SetFontForView<TextView>(view, Resource.Id.registration_email_input_id);
            SetFontForView<Button>(view, Resource.Id.registration_submit_button_id);


            return view;
        }
    }
}