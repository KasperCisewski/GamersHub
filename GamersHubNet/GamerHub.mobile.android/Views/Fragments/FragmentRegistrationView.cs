using System;
using System.Reactive;
using System.Reactive.Linq;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Constants;
using GamerHub.mobile.android.Infrastructure.Extensions;
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
        private TextInputEditText _nameTextEdit;
        private IObservable<EventPattern<TextChangedEventArgs>> _nameTextChangedObservable;
        private IDisposable _nameTextChangedSubscription;

        private TextInputEditText _emailTextEdit;
        private IObservable<EventPattern<TextChangedEventArgs>> _emailTextChangedObservable;
        private IDisposable _emailTextChangedSubscription;

        private TextInputEditText _passwordTextEdit;
        private IObservable<EventPattern<TextChangedEventArgs>> _passwordTextChangedObservable;
        private IDisposable _passwordTextChangedSubscription;

        private TextInputEditText _repeatablePasswordTextEdit;
        private IObservable<EventPattern<TextChangedEventArgs>> _repeatablePasswordTextChangedObservable;
        private IDisposable _repeatablePasswordTextChangedSubscription;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Registration_View, null);

            _nameTextEdit = view.FindViewById<TextInputEditText>(Resource.Id.registration_name_input_id);
            _nameTextChangedObservable = this.InitObservableFromEvent<TextChangedEventArgs, RegistrationViewModel>(_nameTextEdit, "TextChanged");

            _emailTextEdit = view.FindViewById<TextInputEditText>(Resource.Id.registration_email_input_id);
            _emailTextChangedObservable = this.InitObservableFromEvent<TextChangedEventArgs, RegistrationViewModel>(_emailTextEdit, "TextChanged");

            _passwordTextEdit = view.FindViewById<TextInputEditText>(Resource.Id.registration_password_input_id);
            _passwordTextChangedObservable = this.InitObservableFromEvent<TextChangedEventArgs, RegistrationViewModel>(_passwordTextEdit, "TextChanged");

            _repeatablePasswordTextEdit = view.FindViewById<TextInputEditText>(Resource.Id.registration_repeatable_password_input_id);
            _repeatablePasswordTextChangedObservable = this.InitObservableFromEvent<TextChangedEventArgs, RegistrationViewModel>(_repeatablePasswordTextEdit, "TextChanged");

            SetFontForView<TextInputEditText>(view, _nameTextEdit.Id);
            SetFontForView<TextInputEditText>(view, _emailTextEdit.Id);
            SetFontForView<TextInputEditText>(view, _passwordTextEdit.Id);
            SetFontForView<TextInputEditText>(view, _repeatablePasswordTextEdit.Id);
            SetFontForView<TextView>(view, Resource.Id.registration_error_text_view);
            //SetFontForView<TextView>(view, Resource.Id.registration_email_input_id);
            //SetFontForView<TextView>(view, Resource.Id.registration_email_input_id);
            //SetFontForView<TextView>(view, Resource.Id.registration_email_input_id);
            SetFontForView<Button>(view, Resource.Id.registration_submit_button_id);



            return view;
        }

        public override async void OnResume()
        {
            base.OnResume();

            ConfigureSubscriptions();
            _nameTextEdit.RequestFocus();
        }

        public override void OnStop()
        {
            base.OnStop();

            DisposeSubscriptions();
        }

        public void ConfigureSubscriptions()
        {
            _nameTextChangedSubscription =
                _nameTextChangedObservable.Throttle(TimeSpan.FromMilliseconds(UIConstants.DefaultFilterThrottleMiliSeconds))
                    .Subscribe(async e =>
                    {
                        await ViewModel.ValidName();
                    });
            _emailTextChangedSubscription =
                _emailTextChangedObservable.Throttle(TimeSpan.FromMilliseconds(UIConstants.DefaultFilterThrottleMiliSeconds))
                    .Subscribe(async e =>
                    {
                        await ViewModel.ValidEmail();
                    });
            _passwordTextChangedSubscription =
                _passwordTextChangedObservable.Throttle(TimeSpan.FromMilliseconds(UIConstants.DefaultFilterThrottleMiliSeconds))
                    .Subscribe(e =>
                    {
                        ViewModel.ValidPassword();
                    });
            _repeatablePasswordTextChangedSubscription =
                _repeatablePasswordTextChangedObservable.Throttle(TimeSpan.FromMilliseconds(UIConstants.DefaultFilterThrottleMiliSeconds))
                    .Subscribe(e =>
                    {
                        ViewModel.ValidRepeatablePassword();
                    });
        }

        public void DisposeSubscriptions()
        {
            _nameTextChangedSubscription.DisposeIfNotNull();
            _emailTextChangedSubscription.DisposeIfNotNull();
            _passwordTextChangedSubscription.DisposeIfNotNull();
            _repeatablePasswordTextChangedSubscription.DisposeIfNotNull();
        }
    }
}