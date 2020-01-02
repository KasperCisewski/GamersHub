using System;
using System.Reactive;
using System.Reactive.Linq;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using GamerHub.mobile.android.Constants;
using GamerHub.mobile.android.Infrastructure.Extensions;
using GamerHub.mobile.android.Views.Components;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.Infrastructure;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Friends;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentFriendSearchView")]
    public class FragmentFriendSearchView : FragmentBase<FriendSearchViewModel>
    {
        private MvxRecyclerView _recyclerView;
        private RecyclerViewOnScrollListener _scrollListener;
        private TextInputEditText _searchTextEdit;
        private IObservable<EventPattern<TextChangedEventArgs>> _searchTextChangedObservable;
        private IDisposable _searchTextChangedSubscription;
        //ShowFriendCommand
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Friend_Search_View, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.search_friends_list_recycler_view);

            var layoutManager = new GridLayoutManager(Context, 3);
            recyclerView.SetLayoutManager(layoutManager);

            _searchTextEdit = view.FindViewById<TextInputEditText>(Resource.Id.search_friend_input_id);
            _searchTextChangedObservable = this.InitObservableFromEvent<TextChangedEventArgs, FriendSearchViewModel>(_searchTextEdit, "TextChanged");

            SetFontForView<TextInputEditText>(view, _searchTextEdit.Id);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            _recyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.search_friends_list_recycler_view);
            _scrollListener = new RecyclerViewOnScrollListener(StaticAppSettings.PullDataPageSize / 2);
            _scrollListener.LoadMoreEvent += _scrollListener_LoadMoreEvent;
            _recyclerView.AddOnScrollListener(_scrollListener);
        }
        public override void OnResume()
        {
            base.OnResume();

            ConfigureSubscriptions();
            _searchTextEdit.RequestFocus();
        }

        public override void OnStop()
        {
            base.OnStop();
            _scrollListener.LoadMoreEvent -= _scrollListener_LoadMoreEvent;
            DisposeSubscriptions();
        }

        public void ConfigureSubscriptions()
        {
            _searchTextChangedSubscription =
                _searchTextChangedObservable.Throttle(TimeSpan.FromMilliseconds(UIConstants.DefaultFilterThrottleMiliSeconds))
                    .Subscribe(e =>
                    {
                        SearchFriends(true);
                    });
        }

        private void _scrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            SearchFriends(false);
        }

        private void SearchFriends(bool replace)
        {
            Activity.RunOnUiThread(async () =>
            {
                await ViewModel.SearchFriends(replace);
                ViewModel.HideKeyboard();
            });
        }

        public void DisposeSubscriptions()
        {
            _searchTextChangedSubscription.DisposeIfNotNull();
        }
    }
}