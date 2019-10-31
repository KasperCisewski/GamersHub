﻿using Android.OS;
using Android.Runtime;
using Android.Support.Design.Button;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.Login;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentLoginView")]
    public class FragmentLoginView : FragmentBase<LoginViewModel>
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Login_View, null);

            SetFontForView<TextInputEditText>(view, Resource.Id.login_input_id);
            SetFontForView<TextInputEditText>(view, Resource.Id.password_input_id);
            SetFontForView<Button>(view, Resource.Id.login_submit_id);
            SetFontForView<TextView>(view, Resource.Id.login_remember_credentials_view);

            return view;
        }
    }
}