using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using GamerHub.mobile.android.Constants;
using GamerHub.mobile.android.Infrastructure.Extensions;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Search;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using System.Reactive;
using System.Reactive.Linq;
using Android.Support.V7.Widget;
using GamerHub.mobile.android.Views.Components;
using GamerHub.mobile.core.Infrastructure;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentSearchView")]
    public class FragmentSearchView : FragmentBase<SearchViewModel>
    {
        private MvxRecyclerView _recyclerView;
        private RecyclerViewOnScrollListener _scrollListener;
        private TextInputEditText _searchTextEdit;
        private IObservable<EventPattern<TextChangedEventArgs>> _searchTextChangedObservable;
        private IDisposable _searchTextChangedSubscription;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Search_View, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_search_games_list_view);

            var layoutManager = new GridLayoutManager(Context, 2);
            recyclerView.SetLayoutManager(layoutManager);

            _searchTextEdit = view.FindViewById<TextInputEditText>(Resource.Id.search_input_id);
            _searchTextChangedObservable = this.InitObservableFromEvent<TextChangedEventArgs, SearchViewModel>(_searchTextEdit, "TextChanged");

            SetFontForView<TextInputEditText>(view, _searchTextEdit.Id);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            _recyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.recycler_search_games_list_view);
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
                       SearchForGames(true);
                   });
        }

        private void _scrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            SearchForGames(false);
        }

        private void SearchForGames(bool replace)
        {
            Activity.RunOnUiThread(async () =>
            {
                await ViewModel.SearchGames(replace);
                ViewModel.HideKeyboard();
            });
        }

        public void DisposeSubscriptions()
        {
            _searchTextChangedSubscription.DisposeIfNotNull();
        }
    }
}