﻿using System;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GamerHub.mobile.android.Views.Components;
using GamerHub.mobile.android.Views.Fragments.Base;
using GamerHub.mobile.core.Infrastructure;
using GamerHub.mobile.core.ViewModels;
using GamerHub.mobile.core.ViewModels.CoreApp.Games;
using MvvmCross;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GamerHub.mobile.android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_frame_layout, true)]
    [Register("GamerHub.mobile.Android.Views.Fragments.FragmentGamesCategoryView")]
    public class FragmentGamesCategoryView : FragmentBase<GamesCategoryViewModel>
    {
        private MvxRecyclerView _recyclerView;
        private RecyclerViewOnScrollListener _scrollListener;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.Fragment_Games_Category_List, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_games_category_list_view);

            var layoutManager = new GridLayoutManager(Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity, 2);
            recyclerView.SetLayoutManager(layoutManager);

            SetFontForView<TextView>(view, Resource.Id.title_text_view);
            SetFontsForSharedMenuBar(view);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            _recyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.recycler_games_category_list_view);
            _scrollListener = new RecyclerViewOnScrollListener(StaticAppSettings.PullDataPageSize / 2);
            _scrollListener.LoadMoreEvent += _scrollListener_LoadMoreEvent;
            _recyclerView.AddOnScrollListener(_scrollListener);
            SearchForGames(true);
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
    }
}